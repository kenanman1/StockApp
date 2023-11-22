using Entities.IdentityEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockApp.Model;
using StockApp.Services;

namespace StockApp.Controllers;

[Route("[controller]")]
public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IEmailService _emailService;
    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailService = emailService;
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
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);

                await _emailService.SendEmailAsync(model.Email, "Confirm your email",
                    $"Please confirm your account by <a href='{callbackUrl}'>clicking here</a>.");

                return View("EmailSent");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("Email", error.Description);
            }
        }
        return View(model);
    }

    [HttpGet]
    [Authorize("NotAuthorized")]
    [Route("[action]")]
    public async Task<IActionResult> ConfirmEmail(string userId, string code)
    {
        if (userId == null || code == null)
            return RedirectToAction("Error", "Home");

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return RedirectToAction("Error", "Home");

        var result = await _userManager.ConfirmEmailAsync(user, code);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "User");
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Trade");
        }

        return RedirectToAction("Error", "Home");
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

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && !user.EmailConfirmed && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                ModelState.AddModelError("Email", "Email not confirmed yet");
                return View(model);
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
}
