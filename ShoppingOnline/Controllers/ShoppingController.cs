using Microsoft.AspNetCore.Mvc;
using ShoppingOnline.Models;
using ShoppingOnline.Models.ViewModals;

namespace ShoppingOnline.Controllers;

public class ShoppingController : Controller
{
    IProductInterface productRepository;
    
    public ShoppingController(IProductInterface _productRepository)
    {
        productRepository = _productRepository;
    }
    
    // GET
    public IActionResult Index()
    {
        return View("ProductList");
    }
    
    public IActionResult List(string category)
    {
        ProductListViewModel productListViewModel = new ProductListViewModel()
        {
            Products = productRepository.GetAllProducts().Where(p => category == null || p.Category == category).ToList(),
            CurrentCategory = category
        };
        return View("ProductList", productListViewModel);
    }
}