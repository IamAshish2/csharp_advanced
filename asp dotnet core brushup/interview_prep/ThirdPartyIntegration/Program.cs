using System.Text;
using interview_prep.Configuration;
using interview_prep.Context;
using interview_prep.Models;
using interview_prep.Models.user;
using interview_prep.Repositories.Interfaces;
using interview_prep.Repositories.Repository;
using interview_prep.Services.Interfaces;
using interview_prep.Services.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add sql server connection
var connectionString = builder.Configuration["DbConnection:ConnectionString"];
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(connectionString));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JWT"));
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("Cloudinary"));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    // options.Password.RequireDigit = false;
    // options.Password.RequiredLength = 6;
    // options.Password.RequireNonAlphanumeric = false;
    // options.Password.RequireUppercase = false;
    // options.Password.RequireLowercase = false;
}).AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// jwt authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
        {
            var jwtSettings = builder.Configuration.GetSection("JWT").Get<JwtSettings>();
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidAudience = jwtSettings?.Audience,
                ValidIssuer = jwtSettings?.Issuer,
                ValidateAudience = true,
                ValidateActor = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Key ?? "")),
                ValidateIssuerSigningKey = true,
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.Zero
            };
        }
    ).AddCookie(options =>
    {
        options.LoginPath = "/user-login";
        options.LogoutPath = "/";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.Cookie.Name = "interview";
        options.Cookie.HttpOnly = true;
        //the cookie is sent with requests from the same site and during top-level navigations to the cookie's domain
        //from a third-party site, helping to mitigate Cross-Site Request Forgery (CSRF) attacks. 
        options.Cookie.SameSite = SameSiteMode.None;
    });

builder.Services.AddControllers();

// adding services as dependency injection
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IApplicationRepository,ApplicationRepository>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<ICookieService, CookieService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();