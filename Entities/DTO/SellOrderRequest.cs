using System.ComponentModel.DataAnnotations;

namespace StockApp.DTO;

public class SellOrderRequest
{
    [Required]
    public string StockSymbol { get; set; }
    [Required]
    public string StockName { get; set; }
    public DateTime DateTime { get; set; }
    [Range(1, 100000)]
    public int Quantity { get; set; }
    [Range(0.01, 1000000)]
    public double Price { get; set; }
}
