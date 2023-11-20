using StockApp.Model;

namespace Repository;

public interface IStockRepository
{
    Task<BuyOrder> CreateBuyOrderAsync(BuyOrder buyOrder);

    Task<SellOrder> CreateSellOrderAsync(SellOrder sellOrder);

    Task<List<BuyOrder>> GetBuyOrdersAsync(string? userId = null);

    Task<List<SellOrder>> GetSellOrdersAsync(string? userId = null);
}
