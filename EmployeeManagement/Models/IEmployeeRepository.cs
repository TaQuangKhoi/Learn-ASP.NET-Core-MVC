namespace EmployeeManagement.Models;

public interface IEmployeeRepository
{
    // Khai báo phương thức trả về  chi tiết nhan viên theo ID
    Employee GetEmployee(int ID);
    IEnumerable<Employee> GetAllEmployees();
}