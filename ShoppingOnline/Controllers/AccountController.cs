using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingOnline.Models.ViewModals;

namespace ShoppingOnline.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    
    private readonly SignInManager<IdentityUser> _signInManager;
    
    private readonly ILogger<AccountController> _logger;
    
    private readonly RoleManager<IdentityRole> _roleManager;

    private Utils _utils = null;
    
    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
        ILogger<AccountController> logger,
        RoleManager<IdentityRole> roleManager,
        Utils utils
            )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
        _roleManager = roleManager;
        _utils = utils;
        
        // Create User Role
        string roleName = "Users";
        IdentityResult roleResult = null;

        // Check to see if Role Exists, if not create it
        var roleExist = _roleManager.RoleExistsAsync(roleName).Result;
        if (!roleExist)
        {
            // Create the roles and seed them to the database
            roleResult = _roleManager.CreateAsync(new IdentityRole(roleName)).Result;
            // Check result
            if (!roleResult.Succeeded)
            {
                _logger.LogError("Error while creating role");
            }
        }
    }

    /// <summary>
    /// GET: /Account/Register
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Register()
    {
        if (_utils.IsSignedIn(User))
        {
            return RedirectToAction("List", "Shopping");
        }
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
                _logger.LogInformation("User created a new account with password.");
                // add user to Role User
                try
                {
                    await _userManager.AddToRoleAsync(user, "Users");
                    
                    _logger.LogInformation("User added to role Users");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    // after successfully register, sign in user
                    return RedirectToAction("List", "Shopping");
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    await _userManager.DeleteAsync(user);
                }
                finally
                {
                    _logger.LogInformation("Have try to add user to role Users");
                }
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }

    [HttpGet("api/get-customer")]
    public IActionResult Login()
    {
        if (_utils.IsSignedIn(User))
        {
            return RedirectToAction("List", "Shopping");
        }
        return View();
    }

    [HttpPost("api/login")]
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
                    return RedirectToAction("List", "Shopping");
                }
            }
            TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(model);
        }

        TempData["Error"] = "Wrong credentials. Please, try again!";
        return View(model);
    }

    public async Task<IActionResult> SignOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("List", "Shopping");
    }
    
    [HttpPost]
    public IActionResult AdminCreateUser()
    {
        return View();
    }
}