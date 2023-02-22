using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CompanyManagement.Models.Employees
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //use this to configure the contex
        }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //use this to configure the model
        }
        
        // Tên tương tự với bảng trong CSDL
        public DbSet<Employee> Employees { get; set; }
    }
    
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            ConfigurationManager config = new ConfigurationManager();
            var connectionString = config.GetConnectionString("LocalDb");
            Console.WriteLine("Thử nghiệm");
            Console.WriteLine("connectionString: " + connectionString);
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=CompanyManagement;Trusted_Connection=True;MultipleActiveResultSets=true"
                );

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
