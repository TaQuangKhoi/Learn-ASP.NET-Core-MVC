using CompanyManagement.Models.Employees;
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
            Employee employee = _employeeRepository.GetEmployee(ID);
            ViewData["PageTittle"] = "Employee Details";
            return View(employee);
        }
        
        // Phương thức trả về danh sách các nhân viên
        public IActionResult List()
        {
            // return a view with IEnumerable<Employee>
            ViewBag.PageTitle = "Employee List";
            return View(_employeeRepository.GetAllEmployees());
        }

        public string Hello()
        {
            return "Hello";
        }
    }
}
