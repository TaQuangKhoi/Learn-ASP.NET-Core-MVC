using Microsoft.AspNetCore.Mvc;
using ProductManager.Models;
using ProductManager.Models.ViewModels;
using ProductManager.Models.Products;

namespace ProductManager.Controllers
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
            return View();
        }
        
        [HttpPost] // Khi submit thì sẽ thực hiện hành động
        public IActionResult AddProduct(CreateViewModel model)
        {
            string fileName = null;
            string path = _webHost.WebRootPath + "\\images\\";
            fileName = model.Photo.FileName;
            
            model.Photo.CopyTo(new FileStream(path + fileName, FileMode.Create));
            
            Product product = new Product()
            {
                Name = model.Name,
                Category = model.Category,
                Price = model.Price,
                PhotoPath = fileName
            };
            
            _productRepository.Add(product);
            ViewData["WebRootPath"] = path;
            return View("List", _productRepository.GetAllProducts());
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
