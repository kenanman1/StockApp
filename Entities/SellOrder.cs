using StockApp.DTO;
using System.ComponentModel.DataAnnotations;

namespace StockApp.Model;

public class SellOrder
{
    public Guid SellOrderId;

    [Required]
    public string StockSymbol;

    [Required]
    public string StockName;

    public DateTime DateAndTimeOfOrder;

    [Required]
    [Range(1, 100000)]
    public int Quantity;

    [Required]
    [Range(0.01, 1000000)]
    public double Price;

    public static SellOrder ToSellOrder(SellOrderRequest request)
    {
        return new SellOrder
        {
            SellOrderId = Guid.NewGuid(),
            StockSymbol = request.StockSymbol,
            StockName = request.StockName,
            DateAndTimeOfOrder = DateTime.Now,
            Quantity = request.Quantity,
            Price = request.Price
        };
    }
}
