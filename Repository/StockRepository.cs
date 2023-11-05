using Microsoft.EntityFrameworkCore;
using StockApp.DataBase;
using StockApp.Model;

namespace Repository;

public class StockRepository : IStockRepository
{
    private StockDbContext _dbContext; 

    public StockRepository(StockDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<BuyOrder> CreateBuyOrderAsync(BuyOrder buyOrder)
    {
        await _dbContext.BuyOrders.AddAsync(buyOrder);
        await _dbContext.SaveChangesAsync();
        return buyOrder;
    }

    public async Task<SellOrder> CreateSellOrderAsync(SellOrder sellOrder)
    {
        await _dbContext.SellOrders.AddAsync(sellOrder);
        await _dbContext.SaveChangesAsync();
        return sellOrder;
    }

    public async Task<List<BuyOrder>> GetBuyOrdersAsync()
    {
        List<BuyOrder> buyOrders = await _dbContext.BuyOrders.AsNoTracking().ToListAsync();
        return buyOrders;
    }

    public async Task<List<SellOrder>> GetSellOrdersAsync()
    {
        List<SellOrder> sellOrders = await _dbContext.SellOrders.AsNoTracking().ToListAsync();
        return sellOrders;
    }
}
