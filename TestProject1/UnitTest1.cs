using StockApp.DTO;
using StockApp.Services;
using Xunit;
using Xunit.Abstractions;

namespace TestProject1
{
    public class UnitTest1
    {
        private IStockService stockService = new StockService();
        private ITestOutputHelper testOutput;
        public UnitTest1(ITestOutputHelper testOutput)
        {
            this.testOutput = testOutput;
        }
        [Fact]
        public void BuyOrder_IsNull()
        {
            BuyOrderRequest? buyOrder = null;
            BuyOrderRequest buyOrder1 = new() { StockSymbol = "MSFT", StockName = "Microsoft", DateTime = DateTime.Now, Price = 10, Quantity = 0 };
            BuyOrderRequest buyOrder2 = new() { Quantity = 100001, Price = 10, DateTime = DateTime.Now, StockName = "MSFT", StockSymbol = "Microsoft" };
            BuyOrderRequest buyOrder3 = new() { Quantity = 100, Price = 0, DateTime = DateTime.Now, StockName = "MSFT", StockSymbol = "Microsoft" };
            BuyOrderRequest buyOrder4 = new() { Quantity = 100, Price = 1000001, DateTime = DateTime.Now, StockName = "MSFT", StockSymbol = "Microsoft" };
            BuyOrderRequest buyOrder5 = new() { Quantity = 1000, Price = 10, DateTime = DateTime.Now, StockName = "MSFT" };

            BuyOrderRequest correct = new() { Quantity = 100, Price = 10, DateTime = DateTime.Now, StockName = "MSFT", StockSymbol = "Microsoft" };

            Assert.Throws<ArgumentNullException>(() =>
            {
                stockService.CreateBuyOrderAsync(buyOrder);
            });

            Assert.Throws<ArgumentException>(() =>
            {
                stockService.CreateBuyOrderAsync(buyOrder1);
            });

            Assert.Throws<ArgumentException>(() =>
            {
                stockService.CreateBuyOrderAsync(buyOrder2);
            });

            Assert.Throws<ArgumentException>(() =>
            {
                stockService.CreateBuyOrderAsync(buyOrder3);
            });

            Assert.Throws<ArgumentException>(() =>
            {
                stockService.CreateBuyOrderAsync(buyOrder4);
            });

            Assert.Throws<ArgumentException>(() =>
            {
                stockService.CreateBuyOrderAsync(buyOrder5);
            });

            Assert.NotNull(stockService.CreateBuyOrderAsync(correct));
        }

        [Fact]
        public void SellOrder_IsNull()
        {
            SellOrderRequest? sellOrder = null;
            SellOrderRequest sellOrder1 = new() { StockSymbol = "MSFT", StockName = "Microsoft", DateTime = DateTime.Now, Price = 10, Quantity = 0 };
            SellOrderRequest sellOrder2 = new() { Quantity = 100001, Price = 10, DateTime = DateTime.Now, StockName = "MSFT", StockSymbol = "Microsoft" };
            SellOrderRequest sellOrder3 = new() { Quantity = 100, Price = 0, DateTime = DateTime.Now, StockName = "MSFT", StockSymbol = "Microsoft" };
            SellOrderRequest sellOrder4 = new() { Quantity = 100, Price = 1000001, DateTime = DateTime.Now, StockName = "MSFT", StockSymbol = "Microsoft" };
            SellOrderRequest sellOrder5 = new() { Quantity = 1000, Price = 10, DateTime = DateTime.Now, StockName = "MSFT" };
            SellOrderRequest sellOrder6 = new() { DateTime = DateTime.Now, StockName = "MSFT", StockSymbol = "Microsoft", Price = 10, Quantity = 100 };
            Assert.Throws<ArgumentNullException>(() =>
            {
                stockService.CreateSellOrderAsync(sellOrder);
            });
            Assert.Throws<ArgumentException>(() =>
            {
                stockService.CreateSellOrderAsync(sellOrder1);
            });
            Assert.Throws<ArgumentException>(() =>
            {
                stockService.CreateSellOrderAsync(sellOrder2);
            });
            Assert.Throws<ArgumentException>(() =>
            {
                stockService.CreateSellOrderAsync(sellOrder3);
            });
            Assert.Throws<ArgumentException>(() =>
            {
                stockService.CreateSellOrderAsync(sellOrder4);
            });
            Assert.Throws<ArgumentException>(() =>
            {
                stockService.CreateSellOrderAsync(sellOrder5);
            });
            Assert.NotNull(stockService.CreateSellOrderAsync(sellOrder6));

        }

        [Fact]
        public void GetBuyOrders()
        {
            Assert.Empty(stockService.GetBuyOrdersAsync());

            BuyOrderRequest buyOrder = new BuyOrderRequest() { DateTime = DateTime.Now, Price = 10, Quantity = 100, StockName = "MSFT", StockSymbol = "Microsoft" };
            BuyOrderResponse orderResponse = stockService.CreateBuyOrderAsync(buyOrder);

            Assert.NotEmpty(stockService.GetBuyOrdersAsync());
            Assert.Contains(orderResponse, stockService.GetBuyOrdersAsync());
        }

        [Fact]
        public void GetSellOrders()
        {
            Assert.Empty(stockService.GetSellOrdersAsync());

            SellOrderRequest sellOrder = new SellOrderRequest() { DateTime = DateTime.Now, Price = 10, Quantity = 100, StockName = "MSFT", StockSymbol = "Microsoft" };
            SellOrderResponse orderResponse = stockService.CreateSellOrderAsync(sellOrder);

            Assert.NotEmpty(stockService.GetSellOrdersAsync());
            Assert.Contains(orderResponse, stockService.GetSellOrdersAsync());
        }
    }
}
