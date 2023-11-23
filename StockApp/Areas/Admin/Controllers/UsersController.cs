using Entities.IdentityEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockApp.Areas.Admin.Models;

namespace StockApp.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[area]")]
[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    public UsersController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    [Route("[action]")]
    public async Task<IActionResult> ShowAll(int page = 1)
    {
        int pageSize = 10;

        List<ApplicationUser> applicationUsers = await _userManager.Users.ToListAsync();
        List<UserViewModel> users = new();
        foreach (var user in applicationUsers)
        {
            UserViewModel userViewModel = new() { FirstName = user.FirstName, LastName = user.LastName, Email = user.Email, Created = user.Created, TotalOperationsCount = user.TotalOperationsCount };
            users.Add(userViewModel);
        }
        var count = users.Count;
        var items = users.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
        IndexViewModel viewModel = new IndexViewModel(items, pageViewModel);
        return View(viewModel);
    }
}
