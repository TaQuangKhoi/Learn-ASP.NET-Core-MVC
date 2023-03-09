using Microsoft.AspNetCore.Mvc;
using ShoppingOnline.Infrastructure;
using ShoppingOnline.Models;
using ShoppingOnline.Models.ViewModals;

namespace ShoppingOnline.Controllers;

public class CartController : Controller
{
    private IProductInterface _productRepository;

    public CartController(IProductInterface productRepository)
    {
        _productRepository = productRepository;
    }
    
    // GET
    public ViewResult Index(string returnUrl)
    {
        return View(new CartIndexViewModel
        {
            Cart = GetCart(),
            ReturnUrl = returnUrl
        });
    }

    public RedirectToActionResult AddToCart(int id, string returnUrl)
    {
        Product product = _productRepository.GetAllProducts().FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            Cart cart = GetCart();
            cart.AddItem(product, 1);
            SaveCart(cart);
        }

        return RedirectToAction("Index", new { returnUrl });
    }

    public RedirectToActionResult RemoveFromCart(int id, string returnUrl)
    {
        Product product = _productRepository.GetAllProducts().FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            Cart cart = GetCart();
            cart.RemoveItem(product);
            SaveCart(cart);
        }

        return RedirectToAction("Index", new { returnUrl });
    }

    public RedirectToActionResult Clear(string returnUrl)
    {
        Cart cart = GetCart();
        cart.Clear();
        SaveCart(cart);
        return RedirectToAction("Index", new { returnUrl });
    }


    private Cart GetCart()
    {
        Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
        return cart;
    }

    private void SaveCart(Cart cart)
    {
        HttpContext.Session.SetJson("Cart", cart);
    }
}