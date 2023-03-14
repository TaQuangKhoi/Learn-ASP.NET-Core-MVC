using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ShoppingOnline.Models.Orders;
using ShoppingOnline.Models.Carts;

namespace ShoppingOnline.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
    // Thiết lập model dùng để tạo DB
    public DbSet<Product> Products { get; set; }
    
    public DbSet<Order> Orders { get; set; }
    
    public DbSet<OrderDetail> OrderDetails { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // modelBuilder.Entity<OrderDetail>().HasKey(od => new {od.OrderId, od.ProductId});
        
        modelBuilder.Ignore<CartItem>();
        
    }
    
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            
            optionsBuilder.UseSqlite(
                "Data Source=" +
                       Path.Combine(Directory.GetCurrentDirectory(), "mydb.db")
            );
            
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}