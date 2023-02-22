namespace CompanyManagement.Models.Employees;

public interface IEmployeeRepository
{
    // Khai báo phương thức trả về  chi tiết nhan viên theo ID
    Employee GetEmployee(int ID);
    
    IEnumerable<Employee> GetAllEmployees();

    Employee Add(Employee employee);

    Employee Update(Employee employee);

    Employee Delete(Employee employee);
}