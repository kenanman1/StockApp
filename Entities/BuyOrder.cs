using Entities.IdentityEntities;
using StockApp.DTO;
using System.ComponentModel.DataAnnotations;

namespace StockApp.Model;

public class BuyOrder
{
    public Guid BuyOrderId;

    [Required]
    public string StockSymbol { get; set; }

    [Required]
    public string StockName { get; set; }

    public DateTime DateAndTimeOfOrder { get; set; }

    [Required]
    [Range(1, 100000)]
    public int Quantity { get; set; }

    [Required]
    [Range(0.01, 1000000)]
    public double Price { get; set; }

    public ApplicationUser ApplicationUser { get; set; }
    public string UserId { get; set; }

    public static BuyOrder ToBuyOrder(BuyOrderRequest request)
    {
        return new BuyOrder
        {
            BuyOrderId = Guid.NewGuid(),
            StockSymbol = request.StockSymbol,
            StockName = request.StockName,
            DateAndTimeOfOrder = DateTime.Now,
            Quantity = request.Quantity,
            Price = request.Price
        };
    }
}
