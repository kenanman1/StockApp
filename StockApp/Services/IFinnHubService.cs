using StockApp.Model;

namespace StockApp.Services;

public interface IFinnHubService
{
    Task<Dictionary<string, object>> GetCompanyProfile(string? stockSymbol = null);
    Task<Dictionary<string, object>> GetStockPriceQuote(string? stockSymbol = null); 
    Task<List<Stock>> GetStocks(bool showOnlyPopular);
    Task<Dictionary<string, object>> SearchStock(string symbol);
    Task<StockInfo?> GetStockInfo(string symbol);
}
