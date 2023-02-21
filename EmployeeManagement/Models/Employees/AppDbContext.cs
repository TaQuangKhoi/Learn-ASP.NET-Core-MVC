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
            ConfigurationManager config = new ConfigurationManager();
            var connectionString = config.GetConnectionString("LocalDb");
            Console.WriteLine("Thử nghiệm");
            Console.WriteLine("connectionString: " + connectionString);
            optionsBuilder.UseSqlServer(
                connectionString
                );

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
