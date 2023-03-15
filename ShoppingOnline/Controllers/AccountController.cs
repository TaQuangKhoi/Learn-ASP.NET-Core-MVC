﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingOnline.Models.ViewModals;

namespace ShoppingOnline.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    
    private readonly SignInManager<IdentityUser> _signInManager;
    
    private readonly ILogger<AccountController> _logger;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
        ILogger<AccountController> logger
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }

    /// <summary>
    /// GET: /Account/Register
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Copy data from RegisterViewModel to IdentityUser
            var user = new IdentityUser
            {
                UserName = model.Email, Email = model.Email
            };

            // thêm user vào bảng AspNetUserRoles
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded) //nếu đăng ký thành công, chuyển đến trang chủ
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Shopping");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null)
        {
            var passwordCheck = await _userManager.CheckPasswordAsync(user, model.Password);
            if (passwordCheck)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Lis", "Shopping");
                }
            }
            TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(model);
        }

        TempData["Error"] = "Wrong credentials. Please, try again!";
        return View(model);
    }
}