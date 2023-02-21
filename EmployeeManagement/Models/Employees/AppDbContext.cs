using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CompanyManagement.Models.Employees
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        // Tên tương tự với bảng trong CSDL
        public DbSet<Employee> Employees { get; set; }
    }
    
    public class YourDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("your connection string");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
