using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PartyInvites.Models;

namespace PartyInvites.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View("MyView");
    }
    
    [HttpGet]
    public IActionResult ResponseForm()
    {
        return View("ResponseForm");
    }
    
    [HttpPost]
    public IActionResult ResponseForm(GuestResponse guestResponse)
    {
        // Using the ModelState.IsValid property to check if the model is valid
        if (!ModelState.IsValid) return View();
        Repository.AddResponse(guestResponse);
        return View("Thanks", guestResponse); //Chuyển đến View Thanks
        // There is a validation error
    }
    
    public IActionResult ListResponses()
    {
        return View(Repository.Responses.Where(r => r.WillAttend == true));
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}