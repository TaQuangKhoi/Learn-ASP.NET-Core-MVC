using CompanyManagement.Models.Employees;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _employeeRepository;
        
        private readonly ILogger _logger;
        
        public EmployeeController(IEmployeeRepository employeeRepository, ILogger<EmployeeController> logger)
        {
            _logger = logger;
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
            IEnumerable<Employee> employees = _employeeRepository.GetAllEmployees();
            
            return View(employees);
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

        [HttpGet]
        public IActionResult UpdateEmployee(int ID)
        {
            _logger.LogInformation("Update Employee HttpGet");
            _logger.LogInformation("ID: " + ID);
            return View("UpdateEmployee", _employeeRepository.GetEmployee(ID));
        }
        
        [HttpPost]
        public IActionResult UpdateEmployee(Employee employee)
        {
            _logger.LogInformation("Update Employee");
            _employeeRepository.Update(employee);
            return View("List", _employeeRepository.GetAllEmployees());
        }
        
        public IActionResult DeleteEmployee(int ID)
        {
            _logger.LogInformation("Delete Employee");
            _employeeRepository.Delete(_employeeRepository.GetEmployee(ID));
            return View("List", _employeeRepository.GetAllEmployees());
        }
    }
}
