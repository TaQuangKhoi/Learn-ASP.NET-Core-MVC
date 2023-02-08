namespace EmployeeManagement.Models;

public class EmployeeRepository : IEmployeeRepository
{
    private List<Employee> _employeeList;

    public EmployeeRepository()
    {
        _employeeList = new List<Employee>
        {
            new Employee() { ID = 1, Name = "Mary", Department = "IT" },
            new Employee() { ID = 2, Name = "John", Department = "HR" },
            new Employee() { ID = 3, Name = "Sam", Department = "IT" },
            new Employee() { ID = 4, Name = "Peter", Department = "HR" },
            new Employee() { ID = 5, Name = "David", Department = "IT" },
            new Employee() { ID = 6, Name = "Sara", Department = "HR" }
        };
    }
    
    public Employee GetEmployee(int ID)
    {
        // Tạo các nhân viên
        return _employeeList.FirstOrDefault(e => e.ID == ID);
    }

    public IEnumerable<Employee> GetAllEmployees()
    {
        return _employeeList;
    }
}