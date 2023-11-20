namespace StockApp.Model;

public class StockInfo
{
    public string StockSymbol { get; set; }

    public string StockName { get; set; }

    public double Price { get; set; }

    public int? Quantity { get; set; }

    public string? Industry { get; set; }

    public string? WebUrl { get; set; }

    public string? Logo { get; set; }
}
