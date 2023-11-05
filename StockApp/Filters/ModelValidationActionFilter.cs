using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StockApp.Controllers;

namespace StockApp.Filters;

/// <summary>
/// Action filter for model validation
/// </summary>
public class ModelValidationActionFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var c = context.Controller as Controller;
        if (!context.ModelState.IsValid)
            context.Result = c.RedirectToAction(nameof(TradeController.Index), "Trade");
    }
}
