using interview_prep.Models.application;
using interview_prep.Models.job;
using interview_prep.Models.user;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace interview_prep.Context;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // for salary percision
        modelBuilder.Entity<Job>()
            .Property(j => j.Salary)
            .HasPrecision(20, 2);
    }


    public DbSet<User> Users { get; set; }
    public DbSet<ProfilePhoto> ProfilePhoto { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Application> Applications { get; set; }
    public DbSet<Cv> Cv { get; set; }
}