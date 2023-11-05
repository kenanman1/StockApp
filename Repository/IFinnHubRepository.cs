namespace Repository;

public interface IFinnHubRepository
{
    Task<Dictionary<string, object>> GetCompanyProfile(string? stockSymbol = null);
    Task<Dictionary<string, object>> GetStockPriceQuote(string? stockSymbol = null);
    Task<List<Dictionary<string, string>>> GetStocks();
    Task<Dictionary<string, object>> SearchStock(string symbol);
}
