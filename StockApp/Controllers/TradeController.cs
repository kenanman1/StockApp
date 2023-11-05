using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using StockApp.DTO;
using StockApp.Filters;
using StockApp.Model;
using StockApp.Services;

namespace StockApp.Controllers;

[ModelValidationActionFilter]
public class TradeController : Controller
{
    private IFinnHubService _finnHubService;
    private IStockService _stockService;
    public TradeController(IFinnHubService finnHubService, IStockService stockService)
    {
        _finnHubService = finnHubService;
        _stockService = stockService;
    }

    [Route("")]
    [Route("[action]/{symbol?}")]
    public async Task<IActionResult> Index(string? symbol = "MSFT")
    {
        StockInfo? stockInfo = await _finnHubService.GetStockInfo(symbol);
        if (stockInfo != null)
            return View(stockInfo);
        else
            return RedirectToAction(nameof(Index), new {symbol = "MSFT"});
    }

    [HttpPost]
    [Route("Buy")]
    public async Task<IActionResult> BuyOrder(BuyOrderRequest buyOrder)
    {
        buyOrder.DateTime = DateTime.Now;
        await _stockService.CreateBuyOrderAsync(buyOrder);
        return RedirectToAction(nameof(Orders));
    }

    [HttpPost]
    [Route("Sell")]
    public async Task<IActionResult> SellOrder(SellOrderRequest sellOrder)
    {
        sellOrder.DateTime = DateTime.Now;
        await _stockService.CreateSellOrderAsync(sellOrder);
        return RedirectToAction(nameof(Orders));
    }

    [Route("[action]")]
    public async Task<IActionResult> Orders()
    {
        List<BuyOrderResponse> buyOrders = await _stockService.GetBuyOrdersAsync();
        List<SellOrderResponse> sellOrders = await _stockService.GetSellOrdersAsync();

        Orders orders = new() { BuyOrders = buyOrders, SellOrders = sellOrders };
        return View(orders);
    }

    [Route("[action]")]
    public async Task<IActionResult> OrdersToPdf()
    {
        List<BuyOrderResponse> buyOrders = await _stockService.GetBuyOrdersAsync();
        List<SellOrderResponse> sellOrders = await _stockService.GetSellOrdersAsync();

        Orders orders = new() { BuyOrders = buyOrders, SellOrders = sellOrders };
        return new ViewAsPdf(orders)
        {
            FileName = "AllOrders.pdf",
            PageSize = Size.A4,
            PageOrientation = Orientation.Portrait,
        };
    }
}