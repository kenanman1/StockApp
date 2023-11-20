using StockApp.DTO;

namespace StockApp.Model;

public class Orders
{
    public List<BuyOrderResponse> BuyOrders { get; set; }
    public List<SellOrderResponse> SellOrders { get; set; }
}
//это нужно потому что я в view не могу сделать @model List<BuyOrderResponse> и @model List<SellOrderResponse> одновременно
// в общем я это в view отправляю как Orders и там уже разбираю на два списка