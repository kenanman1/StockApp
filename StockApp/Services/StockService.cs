using Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Repository;
using StockApp.DTO;
using StockApp.Model;
using System.ComponentModel.DataAnnotations;

namespace StockApp.Services;

public class StockService : IStockService
{
    private IStockRepository _dbContext;
    private UserManager<ApplicationUser> _userManager;
    private IHttpContextAccessor _httpContextAccessor;
    public StockService(IStockRepository dbContext, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<BuyOrderResponse> CreateBuyOrderAsync(BuyOrderRequest? buyOrderRequest)
    {
        if (buyOrderRequest == null)
        {
            throw new ArgumentNullException(nameof(buyOrderRequest));
        }

        CheckValidation(buyOrderRequest);

        BuyOrder buyOrder = BuyOrder.ToBuyOrder(buyOrderRequest);
        ApplicationUser user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        buyOrder.UserId = user.Id;
        user.TotalOperationsCount++;
        await _userManager.UpdateAsync(user);
        await _dbContext.CreateBuyOrderAsync(buyOrder);
        return BuyOrderResponse.ToBuyOrderResponce(buyOrder);
    }

    public async Task<SellOrderResponse> CreateSellOrderAsync(SellOrderRequest? sellOrderRequest)
    {
        if (sellOrderRequest == null)
        {
            throw new ArgumentNullException(nameof(sellOrderRequest));
        }

        CheckValidation(sellOrderRequest);

        SellOrder sellOrder = SellOrder.ToSellOrder(sellOrderRequest);
        ApplicationUser user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        sellOrder.UserId = user.Id;
        user.TotalOperationsCount++;
        await _userManager.UpdateAsync(user);
        await _dbContext.CreateSellOrderAsync(sellOrder);
        return SellOrderResponse.ToSellOrderResponce(sellOrder);
    }

    public async Task<List<BuyOrderResponse>> GetBuyOrdersAsync()
    {
        ApplicationUser user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        List<BuyOrder> buyOrders = await _dbContext.GetBuyOrdersAsync(user.Id);
        return buyOrders.Select(buyOrder => BuyOrderResponse.ToBuyOrderResponce(buyOrder)).ToList();
    }

    public async Task<List<SellOrderResponse>> GetSellOrdersAsync()
    {
        ApplicationUser user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        List<SellOrder> sellOrders = await _dbContext.GetSellOrdersAsync(user.Id);
        return sellOrders.Select(sellOrder => SellOrderResponse.ToSellOrderResponce(sellOrder)).ToList();
    }

    public void CheckValidation(object obj)
    {
        ValidationContext validation = new(obj);
        List<ValidationResult> results = new();
        if (!Validator.TryValidateObject(obj, validation, results, true))
        {
            throw new ArgumentException(results[0].ErrorMessage);
        }
    }
}
