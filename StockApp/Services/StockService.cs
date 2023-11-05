using Repository;
using StockApp.DTO;
using StockApp.Model;
using System.ComponentModel.DataAnnotations;

namespace StockApp.Services;

public class StockService : IStockService
{
    private IStockRepository dbContext;
    public StockService(IStockRepository dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<BuyOrderResponse> CreateBuyOrderAsync(BuyOrderRequest? buyOrderRequest)
    {
        if (buyOrderRequest == null)
        {
            throw new ArgumentNullException(nameof(buyOrderRequest));
        }

        CheckValidation(buyOrderRequest);

        BuyOrder buyOrder = BuyOrder.ToBuyOrder(buyOrderRequest);
        await dbContext.CreateBuyOrderAsync(buyOrder);
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
        await dbContext.CreateSellOrderAsync(sellOrder);
        return SellOrderResponse.ToSellOrderResponce(sellOrder);
    }

    public async Task<List<BuyOrderResponse>> GetBuyOrdersAsync()
    {
        List<BuyOrder> buyOrders = await dbContext.GetBuyOrdersAsync();
        return buyOrders.Select(buyOrder => BuyOrderResponse.ToBuyOrderResponce(buyOrder)).ToList();
    }

    public async Task<List<SellOrderResponse>> GetSellOrdersAsync()
    {
        List<SellOrder> sellOrders = await dbContext.GetSellOrdersAsync();
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

