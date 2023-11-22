using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StockApp.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[area]")]
[Authorize(Roles = "Admin")]
public class HomeController : Controller
{
    [Route("[action]")]
    public IActionResult Index()
    {
        return View();
    }
}
