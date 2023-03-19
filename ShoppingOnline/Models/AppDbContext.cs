using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ShoppingOnline.Models.Orders;
using ShoppingOnline.Models.Carts;

namespace ShoppingOnline.Models;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
    // Thiết lập model dùng để tạo DB
    public DbSet<Product> Products { get; set; }
    
    public DbSet<Order> Orders { get; set; }
    
    public DbSet<OrderDetail> OrderDetails { get; set; }
    // Thiết lập model dùng để tạo DB
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // modelBuilder.Entity<OrderDetail>().HasKey(od => new {od.OrderId, od.ProductId});
        
        modelBuilder.Ignore<CartItem>();
        
        // Make add Migration work
        modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(x => new { x.UserId });
        
        // Lúc đầu Sơn chỉ set key là Role.Id nên không dùng chức năng thêm Role được
        modelBuilder.Entity<IdentityUserRole<string>>().HasKey(x => new { x.RoleId, x.UserId });
        
        modelBuilder.Entity<IdentityUserToken<string>>().HasKey(x => new { x.UserId, x.Value });
        // Make add Migration work
        
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