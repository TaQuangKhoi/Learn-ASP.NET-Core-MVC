namespace CompanyManagement.Models.Employees;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly List<Employee> _employeeList;

    private readonly  AppDbContext _context;
    
    private readonly ILogger _logger;

    public EmployeeRepository(AppDbContext context, ILogger<EmployeeRepository> logger)
    {
        _logger = logger;
        _employeeList = new List<Employee>
        {
            new Employee() { ID = 1, Name = "Mary", Department = "IT" },
            new Employee() { ID = 2, Name = "John", Department = "HR" },
            new Employee() { ID = 3, Name = "Sam", Department = "IT" },
            new Employee() { ID = 4, Name = "Peter", Department = "HR" },
            new Employee() { ID = 5, Name = "David", Department = "IT" },
            new Employee() { ID = 6, Name = "Sara", Department = "HR" }
        };
        _context = context;
    }

    public Employee GetEmployee(int ID)
    {
        // Tạo các nhân viên
        _logger.LogInformation("Get Employee");
        return  _context.Employees.Find(ID) ;
    }
    

    public IEnumerable<Employee> GetAllEmployees()
    {
        _logger.LogInformation("Get All Employees");
        return _context.Employees;
    }

    public Employee Add(Employee employee)
    {
        _context.Employees.Add(employee);
        _context.SaveChanges();
        return employee;
    }

    public Employee Update(Employee employee)
    {
        throw new NotImplementedException();
    }

    public Employee Delete(Employee employee)
    {
        throw new NotImplementedException();
    }
}