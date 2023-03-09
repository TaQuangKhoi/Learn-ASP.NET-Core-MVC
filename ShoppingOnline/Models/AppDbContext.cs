using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ShoppingOnline.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
    // Thiết lập model dùng để tạo DB
    public DbSet<Product> Products { get; set; }
    
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            
            optionsBuilder.UseSqlite(
                "Data Source=" +
                       Path.Combine(Directory.GetCurrentDirectory(), "Data\\mydb.db")
            );
            
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}