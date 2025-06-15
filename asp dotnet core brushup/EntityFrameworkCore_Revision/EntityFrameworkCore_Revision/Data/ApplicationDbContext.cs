

using EntityFrameworkCore_Revision.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore_Revision.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public DbSet<Order> Order{ get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // P.S FOR REAL WORLD PROJECTS, WE STORE THE CONNECTION STRING PRIVATELY


            // for use with sql server
            // optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-CA3R21A\SQLEXPRESS;Initial Catalog=PizzaDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");


            // for use with Sqlite
            // optionsBuilder.UseSqlite(@"Data Source=FoodFaze.db");
            // Add-Migration {}
            // Update-Database
            // Add the following to the project csproj. It will start the project from the build directory. 
            // That way the database file is found.
            // 	<StartWorkingDirectory>${MsBuildProjectDirectory}</StartWorkingDirectory>




            // for use with postgresql
            optionsBuilder.UseNpgsql("User ID=postgres;Password=admin;Host=localhost;Port=5432;Database=foodDb;Pooling=true;MinPoolSize=0;MaxPoolSize=100;ConnectionLifetime=0;");
        }
    }
}
