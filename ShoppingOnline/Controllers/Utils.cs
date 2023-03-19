using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace ShoppingOnline.Controllers;

public class Utils
{
    private readonly RoleManager<IdentityRole> _roleManager;
    
    private readonly UserManager<IdentityUser> _userManager;

    private readonly ILogger<AccountController> _logger;
    
    private IList<string> roles = null;
    
    private IdentityUser user = null;
    
    public Utils(
        RoleManager<IdentityRole> roleManager,
        ILogger<AccountController> logger,
        UserManager<IdentityUser> userManager
    )
    {
        _roleManager = roleManager;
        _logger = logger;
        _userManager = userManager;
    }
    
    public bool IsAdmin(ClaimsPrincipal User)
    {
        _logger.LogInformation("IsAdmin");
        if (!IsSignedIn(User))
        {
            return false;
        }
        roles = _userManager.GetRolesAsync(user).Result;
        if (roles.Contains("Admins"))
        {
            return true;
        }
        return false;
    }
    
    public bool IsSignedIn(ClaimsPrincipal User)
    {
        _logger.LogInformation("IsSignedIn");
        user = _userManager.GetUserAsync(User).Result;
        if (user != null)
        {
            _logger.LogInformation("User is signed in");
            return true;
        }
        return false;
    }
}
