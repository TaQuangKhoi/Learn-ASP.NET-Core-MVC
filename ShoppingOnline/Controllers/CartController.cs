﻿using Microsoft.AspNetCore.Mvc;
using ShoppingOnline.Infrastructure;
using ShoppingOnline.Models;
using ShoppingOnline.Models.Carts;
using ShoppingOnline.Models.ViewModals;

namespace ShoppingOnline.Controllers;

public class CartController : Controller
{
    private readonly  IProductInterface _productRepository;
    private ILogger<CartController> _logger;

    public CartController(IProductInterface productRepository, ILogger<CartController> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
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
        Cart cart = HttpContext.Session.GetJson<Cart>("Cart");
        if (cart == null)
        {
            cart = new Cart();
            SaveCart(cart);
        }
        return cart;
    }

    private void SaveCart(Cart cart)
    {
        HttpContext.Session.SetJson("Cart", cart);
    }
    
    /// <summary>
    /// Deletes a specific TodoItem.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private void UpdateQuantity(int id, int quantity = 1)
    {
        _logger.LogInformation("UpdateQuantity() called");
        _logger.LogInformation($"id: {id}, quantity: {quantity}");
        Product product = _productRepository.GetAllProducts().FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            Cart cart = GetCart();
            cart.SetQuantity(product, quantity);
            SaveCart(cart);
        }
    }
}