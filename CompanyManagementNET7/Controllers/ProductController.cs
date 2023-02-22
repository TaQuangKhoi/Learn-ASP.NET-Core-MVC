using CompanyManagement.Models.Products;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagement.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
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
