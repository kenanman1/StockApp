using Entities.IdentityEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockApp.Areas.User.Models;

namespace StockApp.Areas.User.Controllers;

[Area("User")]
[Route("[area]")]
[Authorize]

public class UserController : Controller
{
    private UserManager<ApplicationUser> _userManager;
    public UserController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> Profile()
    {
        ApplicationUser user = await _userManager.FindByEmailAsync(User.Identity.Name);
        UserViewModel userViewModel = new UserViewModel { FirstName = user.FirstName, LastName = user.LastName, Email = user.Email, Created = user.Created, TotalOperationsCount = user.TotalOperationsCount };
        return View(userViewModel);
    }
}
