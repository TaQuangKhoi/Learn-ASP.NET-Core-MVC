using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ShoppingOnline.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
    public DbSet<Product> Products { get; set; }
    
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            
            optionsBuilder.UseSqlite(
                "Data Source=c:\\mydb.db;Version=3;"
            );
            
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}