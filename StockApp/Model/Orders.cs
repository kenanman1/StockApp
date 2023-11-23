using StockApp.DTO;

namespace StockApp.Model;

public class Orders
{
    public List<BuyOrderResponse> BuyOrders { get; set; }
    public List<SellOrderResponse> SellOrders { get; set; }
}