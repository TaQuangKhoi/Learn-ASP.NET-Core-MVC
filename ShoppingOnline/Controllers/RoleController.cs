using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingOnline.Controllers;

public class RoleController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;

    private readonly UserManager<IdentityUser> _userManager;

    private readonly ILogger<AccountController> _logger;

    private IList<string> roles = null;

    private IdentityUser user = null;

    private Utils _utils = null;

    public RoleController(
        RoleManager<IdentityRole> roleManager,
        ILogger<AccountController> logger,
        UserManager<IdentityUser> userManager,
        Utils utils
    )
    {
        _roleManager = roleManager;
        _logger = logger;
        _userManager = userManager;
        _utils = utils;

        // Create Admin Role
        string roleName = "Admins";
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

    public IActionResult RoleList()
    {
        if (!_utils.IsAdmin(User))
        {
            return RedirectToAction("List", "Shopping");
        }

        var roles = _roleManager.Roles.ToList();
        return View(roles);
    }
}
