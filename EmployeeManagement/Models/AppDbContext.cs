using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using CompanyManagement.Models.Employees;

namespace CompanyManagement.Models
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
            
            Console.WriteLine("Thử nghiệm");
            
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=EmployeesDB;" +
                "Trusted_Connection=True;MultipleActiveResultSets=true"
                );
            
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
