using Entities.IdentityEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockApp.Model;

namespace StockApp.Controllers;

[Route("[controller]")]
public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    [Authorize("NotAuthorized")]
    [HttpGet]
    [Route("[action]")]
    public IActionResult SignUp()
    {
        return View();
    }

    [Authorize("NotAuthorized")]
    [HttpPost]
    [Route("[action]")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SignUp(SignUpViewModel model)
    {
        await CreateRoles();
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Created = DateTimeOffset.UtcNow
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                if (user.Email == "admin@admin.com")
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Trade");
                }
                await _userManager.AddToRoleAsync(user, "User");
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Trade");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("Email", error.Description);
            }
        }
        return View(model);
    }

    [Authorize("NotAuthorized")]
    [HttpGet]
    [Route("[action]")]
    public IActionResult SignIn()
    {
        return View();
    }

    [Authorize("NotAuthorized")]
    [HttpPost]
    [Route("[action]")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SignIn(SignInViewModel model, string? ReturnUrl)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(ReturnUrl))
                    return LocalRedirect(ReturnUrl);
                return RedirectToAction("Index", "Trade");
            }
            ModelState.AddModelError("Email", "Incorrect email or password");
        }
        return View(model);
    }

    [Authorize]
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> CheckEmail(string email)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return Json(true);
        else
            return Json(false);
    }

    private async Task CreateRoles()
    {
        if (!await _roleManager.RoleExistsAsync("Admin"))
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
        if (!await _roleManager.RoleExistsAsync("User"))
            await _roleManager.CreateAsync(new IdentityRole("User"));
    }
}
