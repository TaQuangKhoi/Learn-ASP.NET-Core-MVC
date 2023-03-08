﻿using System.Diagnostics;
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
    public IActionResult OpenUpdate(int id)
    {
        _logger.LogInformation("Open Update Employee");
        _logger.LogInformation("Id: " + id);
        Product product = _productInterface.GetProduct(id);
        UpdateViewModel vm = new UpdateViewModel()
        {
            Id = product.Id,
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
    public IActionResult SaveUpdate(UpdateViewModel vm)
    {
        _logger.LogInformation("Save Update Employee");
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