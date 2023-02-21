using Microsoft.EntityFrameworkCore;

namespace CompanyManagement.Models.Employees
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) {}

        // Tên tương tự với bảng trong CSDL
        public DbSet<Employee> Employees { get; set; }
    }
}
