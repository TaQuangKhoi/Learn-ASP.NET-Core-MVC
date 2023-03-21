using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingOnline.Models;
using ShoppingOnline.Models.ViewModals;

namespace ShoppingOnline.Controllers;

public class ProductManagementController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private IProductInterface _productInterface;

    private IWebHostEnvironment _webHost;
    
    public ProductManagementController(ILogger<HomeController> logger,
        IWebHostEnvironment webHost, IProductInterface productInterface)
    {
        _logger = logger;
        _webHost = webHost;
        _productInterface = productInterface;
    }
    
    
    // GET
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
    public IActionResult Create(CreateViewModel? model)
    {
        if (ModelState.IsValid)
        {
            string fileName = "";
            if (model.Image != null)
            {
                IFormFile image = model.Image;
                ProcessImage(image, ref fileName);
            }
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
        _logger.LogInformation("Delete Employee");
        _logger.LogInformation("Id: " + id);
        _productInterface.DeleteProduct(id);
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Update(int id)
    {
        _logger.LogInformation("Open Update Employee");
        _logger.LogInformation("Id: " + id);
        Product product = _productInterface.GetProduct(id);
        _logger.LogInformation("Product Id: " + product.Id);
        UpdateViewModel vm = new UpdateViewModel()
        {
            Id = id,
            Name = product.Name,
            ShortDescription = product.ShortDescription,
            LongDescription = product.LongDescription,
            Category = product.Category,
            Price = product.Price,
            Stock = product.Stock,
            // ImageUrl = product.ImageUrl
        };
        return View("Update", vm);
    }
        
    [HttpPost]
    public IActionResult Update(UpdateViewModel vm)
    {
        ModelState.Remove("Id");
        _logger.LogInformation("Save Update Employee");
        _logger.LogInformation("Save Update Id: " + vm.Id);
        _logger.LogInformation("Save Update vm: " + vm.toStr());
        // if (ModelState.IsValid) {
            _logger.LogInformation("Model is valid");
            string fileName = "";
            if (vm.Image != null)
            {
                IFormFile image = vm.Image;
                ProcessImage(image, ref fileName);
            }
            
            Product product = new Product()
            {
                Id = vm.Id,
                Name = vm.Name,
                ShortDescription = vm.ShortDescription,
                LongDescription = vm.LongDescription,
                Category = vm.Category,
                Price = vm.Price,
                Stock = vm.Stock,
                ImageUrl = fileName
            };
            _productInterface.UpdateProduct(product);
            return RedirectToAction("Index");
        // }
        // else
        // {
        //     _logger.LogInformation("Model is invalid");
        // }
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    /*
     * Hàm xử lý hình ảnh
     * 
     */
    private void ProcessImage(IFormFile image, ref string fileName)
    {
        string path = _webHost.WebRootPath + "\\images\\";
        fileName = image.FileName;
        image.CopyTo(new FileStream(path + fileName, FileMode.Create));
    }
}