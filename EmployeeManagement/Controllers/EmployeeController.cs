using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _employeeRepository;
        
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IActionResult Details(int ID)
        {
            return View(_employeeRepository.GetEmployee(ID));
        }
        
        // Phương thức trả về danh sách các nhân viên
        public IActionResult List()
        {
            // return a view with IEnumerable<Employee>
            return View(_employeeRepository.GetAllEmployees());
        }
    }
}
