using Microsoft.AspNetCore.Mvc;
using ShoppingOnline.Models.Carts;
using ShoppingOnline.Models.Orders;

namespace ShoppingOnline.Controllers;

public class OrderController : Controller
{
    private ILogger<OrderController> _logger;
    
    private readonly IOrderRepository _orderRepository;

    private Cart _cart;
    
    public OrderController(IOrderRepository orderRepository, Cart cartService, ILogger<OrderController> logger)
    {
        _orderRepository = orderRepository;
        _cart = cartService;
        _logger = logger;
    }
    
    // GET
    [HttpGet]
    public IActionResult Checkout()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Checkout(Order order)
    {
        if ( _cart.Items.Count() == 0 )
        {
            ModelState.AddModelError("", "Sorry, your cart is empty!");
            _logger.LogWarning("Checkout failed: cart is empty");
        }

        if ( ModelState.IsValid )
        {
            _orderRepository.SaveOrder(order);
            return RedirectToAction("Completed");
        }
        else
        {
            return View(order);
        }
    }
    
    public ViewResult Completed()
    {
        _cart.Clear();
        return View();
    }
}