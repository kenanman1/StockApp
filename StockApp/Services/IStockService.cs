using StockApp.DTO;

namespace StockApp.Services;

public interface IStockService
{
    Task<BuyOrderResponse> CreateBuyOrderAsync(BuyOrderRequest? buyOrderRequest);

    Task<SellOrderResponse> CreateSellOrderAsync(SellOrderRequest? sellOrderRequest);

    Task<List<BuyOrderResponse>> GetBuyOrdersAsync();

    Task<List<SellOrderResponse>> GetSellOrdersAsync();
}
