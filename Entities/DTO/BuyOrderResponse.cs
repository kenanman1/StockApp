﻿using StockApp.Model;

namespace StockApp.DTO;

public class BuyOrderResponse
{
    public Guid Guid { get; set; }
    public string StockSymbol { get; set; }
    public string StockName { get; set; }
    public DateTime DateTime { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public double TradeAmount { get; set; }
    public static BuyOrderResponse ToBuyOrderResponce(BuyOrder order)
    {
        return new BuyOrderResponse
        {
            Guid = order.BuyOrderId,
            StockSymbol = order.StockSymbol,
            StockName = order.StockName,
            DateTime = order.DateAndTimeOfOrder,
            Quantity = order.Quantity,
            Price = order.Price,
            TradeAmount = Math.Round(order.Price * order.Quantity, 4)
        };
    }
}

