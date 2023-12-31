﻿using Newtonsoft.Json;
using Repository;
using StockApp.Model;
using System.Globalization;

namespace StockApp.Services;

public class FinnHubService : IFinnHubService
{
    private IFinnHubRepository _finnhubRepository;
    private IConfiguration _configuration;

    public FinnHubService(IConfiguration configuration, IFinnHubRepository finnhubRepository)
    {
        _finnhubRepository = finnhubRepository;
        _configuration = configuration;
    }

    public async Task<Dictionary<string, object>> GetCompanyProfile(string? stockSymbol = null)
    {
        return await _finnhubRepository.GetCompanyProfile(stockSymbol);
    }

    public async Task<Dictionary<string, object>> GetStockPriceQuote(string? stockSymbol = null)
    {
        return await _finnhubRepository.GetStockPriceQuote(stockSymbol);
    }

    public async Task<List<Stock>> GetStocks()
    {
        List<Dictionary<string, string>> stocks = await _finnhubRepository.GetStocks();
        List<Stock> stockList = new();
        for (int i = 0; i < stocks.Count; i++)
        {
            Dictionary<string, string> stock = stocks[i];
            stockList.Add(new Stock() { StockSymbol = stock["symbol"], StockName = stock["description"] });
        }
        string[] popular = _configuration.GetSection("TradingOptions")["Top25PopularStocks"].Split(',');
        stockList = stockList.Where(p => popular.Contains(p.StockSymbol)).ToList();

        return stockList;
    }

    public async Task<Dictionary<string, object>> SearchStock(string symbol)
    {
        return await _finnhubRepository.SearchStock(symbol);
    }

    public async Task<StockInfo?> GetStockInfo(string symbol)
    {
        Dictionary<string, object>? priceQuote = await GetStockPriceQuote(symbol);
        Dictionary<string, object>? companyProfile = await GetCompanyProfile(symbol);

        if (companyProfile != null)
        {
            if (companyProfile.Count > 0)
            {
                string p = priceQuote["c"].ToString();
                double price = double.Parse(p, CultureInfo.InvariantCulture);
                StockInfo stockTrade = new() { Price = price, StockSymbol = symbol, StockName = Convert.ToString(companyProfile["name"]), Industry = Convert.ToString(companyProfile["finnhubIndustry"]), WebUrl = Convert.ToString(companyProfile["weburl"]), Logo = Convert.ToString(companyProfile["logo"]) };
                return stockTrade;
            }

            else
            {
                Dictionary<string, object> companyProfile1 = await SearchStock(symbol);
                string json = companyProfile1["result"].ToString();
                var data = JsonConvert.DeserializeObject<List<StockData>>(json);
                var stock = data.FirstOrDefault(p => p.Symbol == symbol);
                string p = priceQuote["c"].ToString();
                double price = double.Parse(p, CultureInfo.InvariantCulture);
                if (stock != null && data.Count > 0)
                {
                    StockInfo stockTrade = new() { Price = price, StockSymbol = stock.Symbol, StockName = stock.Description };
                    return stockTrade;
                }
                else
                    return null;
            }
        }
        else
            return null;
    }
}
