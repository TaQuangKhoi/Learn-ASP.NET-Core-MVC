﻿using CompanyManagement.Models.Products;
using CompanyManagement.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagement.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _productRepository;
        private IWebHostEnvironment _webHost;
        
        public ProductController(IProductRepository productRepository, IWebHostEnvironment webHost)
        {
            _productRepository = productRepository;
            _webHost = webHost;
        }
        
        [HttpGet] // Khi load lên thì chỉ hiện nơi nhập data
        public IActionResult AddProduct()
        {
            string path = _webHost.ContentRootPath;
            // _logger.LogInformation("ContentRootPath" + path);
            return View();
        }
        
        [HttpPost] // Khi submit thì sẽ thực hiện hành động
        public IActionResult AddProduct(CreateViewModel model)
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
        
        
        
        public IActionResult Details(int ID)
        {
            return View(_productRepository.GetProduct(ID));
        }
        public IActionResult List()
        {
            // return a view with IEnumerable<Employee>
            return View(_productRepository.GetAllProducts());
        }
    }
}
