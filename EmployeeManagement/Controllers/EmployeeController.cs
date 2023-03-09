using CompanyManagement.Models.Employees;
using CompanyManagement.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _employeeRepository;
        private IWebHostEnvironment _webHost;
        
        private readonly ILogger _logger;

        
        public EmployeeController(
            IEmployeeRepository employeeRepository, 
            ILogger<EmployeeController> logger,
            IWebHostEnvironment webHost
            )
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
            _webHost = webHost;
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

        [HttpGet] // Khi load lên thì chỉ hiện nơi nhập data
        public IActionResult AddEmployee()
        {
            string path = _webHost.ContentRootPath;
            // _logger.LogInformation("ContentRootPath" + path);
            return View();
        }
        
        [HttpPost] // Khi submit thì sẽ thực hiện hành động
        public IActionResult AddEmployee(CreateViewModel model)
        {
            _logger.LogInformation("Add Employee");
            string fileName = "";
            string path = _webHost.WebRootPath + "\\images\\";
            
            
            if (model.Photo is null)
            {
                _logger.LogInformation("Photo is null");
                fileName = "null";
            }
            else
            {
                fileName = model.Photo.FileName;
            }
            
            model.Photo.CopyTo(new FileStream(path + fileName, FileMode.Create));
            
            _logger.LogInformation("fileName" + fileName);
            _logger.LogInformation("WebRootPath" + path);

            Employee employee = new Employee()
            {
                Name = model.Name,
                Department = model.Department,
                PhotoPath = fileName
            };
            
            // B1: Thêm 1 nhân viên mới
            _employeeRepository.Add(employee);
            
            //B2: Upload hình của nhân viên lên server
            // Trong thư mục wwwroot, thêm thư mục images
            
            ViewData["WebRootPath"] = path;
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
            _logger.LogInformation("ID: " + employee.ID);
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
