using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Repository;

public class FinnHubRepository : IFinnHubRepository
{
    private IHttpClientFactory HttpClientFactory;
    private IConfiguration configuration;
    public FinnHubRepository(IHttpClientFactory factory, IConfiguration configuration)
    {
        HttpClientFactory = factory;
        this.configuration = configuration;

    }
    public async Task<Dictionary<string, object>> GetCompanyProfile(string? stockSymbol = null)
    {
        if (stockSymbol == null)
        {
            stockSymbol = configuration.GetSection("TradingOptions")["DefaultStockSymbol"];
        }
        HttpClient client = HttpClientFactory.CreateClient();
        HttpRequestMessage requestMessage = new() { Method = HttpMethod.Get, RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token=cjh5fopr01qu5vpthpq0cjh5fopr01qu5vpthpqg") };
        HttpResponseMessage message = await client.SendAsync(requestMessage);
        if (message.IsSuccessStatusCode)
        {
            string content = await new StreamReader(await message.Content.ReadAsStreamAsync()).ReadToEndAsync();
            Dictionary<string, object>? result = JsonSerializer.Deserialize<Dictionary<string, object>>(content);
            return result;
        }
        else
            return null;
    }

    public async Task<Dictionary<string, object>> GetStockPriceQuote(string? stockSymbol = null)
    {
        if (stockSymbol == null)
        {
            stockSymbol = configuration.GetSection("TradingOptions")["DefaultStockSymbol"];
        }
        HttpClient client = HttpClientFactory.CreateClient();
        HttpRequestMessage httpRequest = new() { Method = HttpMethod.Get, RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token=cjh5fopr01qu5vpthpq0cjh5fopr01qu5vpthpqg") };
        HttpResponseMessage message = await client.SendAsync(httpRequest);
        if (message.IsSuccessStatusCode)
        {
            string content = await new StreamReader(await message.Content.ReadAsStreamAsync()).ReadToEndAsync();
            Dictionary<string, object>? result = JsonSerializer.Deserialize<Dictionary<string, object>>(content);
            return result;
        }
        else
            return null;
    }

    public async Task<List<Dictionary<string, string>>> GetStocks()
    {
        HttpClient client = HttpClientFactory.CreateClient();

        HttpRequestMessage httpRequest = new() { Method = HttpMethod.Get, RequestUri = new Uri("https://finnhub.io/api/v1/stock/symbol?exchange=US&token=cjh5fopr01qu5vpthpq0cjh5fopr01qu5vpthpqg") };

        HttpResponseMessage message = await client.SendAsync(httpRequest);
        if (message.IsSuccessStatusCode)
        {
            string content = await message.Content.ReadAsStringAsync();
            List<Dictionary<string, string>>? result = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(content);
            return result;
        }
        else
            return null;
    }

    public async Task<Dictionary<string, object>> SearchStock(string symbol)
    {

        HttpClient client = HttpClientFactory.CreateClient();
        HttpRequestMessage httpRequest = new() { Method = HttpMethod.Get, RequestUri = new Uri($"https://finnhub.io/api/v1/search?q={symbol}&token=cjh5fopr01qu5vpthpq0cjh5fopr01qu5vpthpqg") };

        HttpResponseMessage message = await client.SendAsync(httpRequest);
        if (message.IsSuccessStatusCode)
        {
            string content = await message.Content.ReadAsStringAsync();
            Dictionary<string, object>? result = JsonSerializer.Deserialize<Dictionary<string, object>>(content);
            return result;
        }
        else
            return null;
    }
}
