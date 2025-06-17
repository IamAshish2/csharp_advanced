using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Signal_R.Configuration;
using Signal_R.Data;
using Signal_R.Hubs;
using Signal_R.Models;
using Signal_R.Repository.Interfaces;
using Signal_R.Repository.Repositories;
using Signal_R.Service.Interfaces;
using Signal_R.Service.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var frontendUrl = builder.Configuration["FrontEndUrls:FrontendUrl"];
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllRequests", policy =>
    {
        policy.WithOrigins(frontendUrl ?? "").AllowCredentials().AllowAnyHeader().AllowAnyMethod();
    });
});

// configure models for strictness
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<FrontendUrls>(builder.Configuration.GetSection("FrontEndUrls"));
builder.Services.Configure<GoogleSettings>(builder.Configuration.GetSection("Google"));

// register Identity for use
builder.Services.AddIdentity<User, IdentityRole>(options =>
{

}).AddEntityFrameworkStores<ApplicationDbContext>()
.AddSignInManager<SignInManager<User>>()
   .AddDefaultTokenProviders();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings?.Issuer,

            ValidateAudience = true,
            ValidAudience = jwtSettings?.Audience,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Key)),
            ClockSkew = TimeSpan.Zero
        };
    })
    .AddCookie(options =>
    {
        // Increase cookie expiration to handle the full OAuth flow
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.Cookie.Name = "Cookie";
        options.Cookie.HttpOnly = true;
        //the cookie is sent with requests from the same site and during top-level navigations to the cookie's domain
        //from a third-party site, helping to mitigate Cross-Site Request Forgery (CSRF) attacks. 
        options.Cookie.SameSite = SameSiteMode.None;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.LoginPath = "/api/auth/login";
        options.LogoutPath = "/api/auth/logout";
    })
    .AddGoogle(options =>
    {
        var googleSettings = builder.Configuration.GetSection("Google").Get<GoogleSettings>();
        options.ClientId = googleSettings.ClientId;
        options.ClientSecret = googleSettings.ClientSecret;
        options.SaveTokens = true;
        //options.CallbackPath = "/api/GoogleAuth/google-callBack";
        options.UsePkce = true; // For OAuth2 providers that support it
        //options.AuthorizationEndpoint += "?prompt=consent"; // force consent screen if needed
    });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SignalR API", Description = "SignalR. FastCom", Version = "v1" });

    //  define the security defination of the added security scheme
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Bearer",
        Type = SecuritySchemeType.Http,
        Description = "Please enter the jwt token here for authorization",
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
             {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    },
                },
                new string[]{}
             },
        });
}
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<SharedDb>();
builder.Services.AddScoped<ICookieService, CookieService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json","SignalR Project");
    });
}

app.UseDeveloperExceptionPage();
app.UseCors("AllowAllRequests");
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

app.MapHub<MessageHub>("/messages");

app.Run();

