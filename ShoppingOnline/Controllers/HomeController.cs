using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ShoppingOnline.Models;
using ShoppingOnline.Models.ViewModals;

namespace ShoppingOnline.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private IProductInterface _productInterface;

    private IWebHostEnvironment _webHost;

    public HomeController(ILogger<HomeController> logger,
        IWebHostEnvironment webHost, IProductInterface productInterface)
    {
        _logger = logger;
        _webHost = webHost;
        _productInterface = productInterface;
    }

    public IActionResult Index()
    {
        ViewData["Path"] = _webHost.WebRootPath;
        return View(_productInterface.GetAllProducts());
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(CreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            string path = _webHost.WebRootPath + "\\images\\";
            var fileName = model.Image.FileName;
            model.Image.CopyTo(new FileStream(path + fileName, FileMode.Create));
            Product product = new Product()
            {
                Name = model.Name,
                ShortDescription = model.ShortDescription,
                LongDescription = model.LongDescription,
                Category = model.Category,
                Price = model.Price,
                Stock = model.Stock,
                ImageUrl = fileName
            };
            _productInterface.AddProduct(product);
            return RedirectToAction("Index");
        }
        return View();
    }

    public IActionResult Delete(int id)
    {
        _productInterface.DeleteProduct(id);
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Update(Product product)
    {
        _logger.LogInformation("Update Employee HttpGet");
        // _logger.LogInformation("ID: " + ID);
        UpdateViewModel vm = new UpdateViewModel();
        vm.Product = product;
        return View("Update", vm);
    }
        
    [HttpPost]
    public IActionResult Update(UpdateViewModel vm)
    {
        _logger.LogInformation("Update Employee");
        _productInterface.UpdateProduct(vm.Product);
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}