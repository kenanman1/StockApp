using Microsoft.AspNetCore.Mvc;
using StockApp.Filters;
using StockApp.Model;
using StockApp.Services;

namespace StockApp.Controllers;

[ModelValidationActionFilter]
[Route("[controller]")]
public class StocksController : Controller
{
    private IFinnHubService _finnHubService;
    public StocksController(IFinnHubService finnHubService)
    {
        _finnHubService = finnHubService;
    }

    [Route("[action]")]
    public async Task<IActionResult> Explore()
    {
        List<Stock> stocks = await _finnHubService.GetStocks(true);
        return View(stocks);
    }

    [Route("[action]/{symbol}")]
    public async Task<IActionResult> ExploreSymbol(string symbol)
    {
        List<Stock> allStocks = await _finnHubService.GetStocks(true);
        ViewBag.AllStocks = allStocks;

        StockInfo ?stockInfo = await _finnHubService.GetStockInfo(symbol);
        if (stockInfo != null)
            return View(stockInfo);
        else
            return RedirectToAction(nameof(Explore));
    }

    [Route("[action]")]
    public async Task<IActionResult> Search(string symbol)
    {
        symbol = symbol.ToUpper();
        StockInfo? stockInfo = await _finnHubService.GetStockInfo(symbol);
        if (stockInfo != null)
            return View(stockInfo);
        else
            return RedirectToAction(nameof(TradeController.Index), "Trade", new { symbol = "MSFT" });
    }
}

