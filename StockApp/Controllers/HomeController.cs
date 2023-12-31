﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace StockApp.Controllers;

public class HomeController : Controller
{
    [Route("/")]
    public IActionResult Index()
    {
        return View();
    }

    [Route("/Error")]
    public IActionResult Error()
    {
        if (HttpContext.Features.Get<IExceptionHandlerFeature>() != null)
            return View();
        else
            return RedirectToAction("Index", "Trade");
    }

    [Route("/AccessDenied")]
    public IActionResult AccessDenied()
    {
        return View();
    }
}
