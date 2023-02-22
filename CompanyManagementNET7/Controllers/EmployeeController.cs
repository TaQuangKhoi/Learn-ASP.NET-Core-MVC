using CompanyManagement.Models.Employees;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagement.Controllers
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

        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            _employeeRepository.Add(employee);
            return View("List", _employeeRepository.GetAllEmployees());
        }
        
        // [HttpPost]
        // public IActionResult UpdateEmployee(Employee employee)
        // {
        //     return View( _employeeRepository.Update(employee));
        // }
    }
}
