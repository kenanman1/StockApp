using System.ComponentModel.DataAnnotations;

namespace StockApp.DTO;

public class BuyOrderRequest
{
    [Required]
    public string StockSymbol { get; set; }
    [Required]
    public string StockName { get; set; }
    public DateTime DateTime { get; set; }
    [Range(1, 100000)]
    [DataType(DataType.Currency)]
    public int Quantity { get; set; }
    [Range(0.01, 1000000)]
    public double Price { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        BuyOrderRequest buyOrderRequest = (BuyOrderRequest)obj;
        return buyOrderRequest.StockSymbol == StockSymbol &&
            buyOrderRequest.StockName == StockName &&
            buyOrderRequest.DateTime == DateTime &&
            buyOrderRequest.Quantity == Quantity &&
            buyOrderRequest.Price == Price;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
