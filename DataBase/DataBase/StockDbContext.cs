using Microsoft.EntityFrameworkCore;
using StockApp.Model;

namespace StockApp.DataBase;

public class StockDbContext : DbContext
{
    public StockDbContext(DbContextOptions<StockDbContext> options) : base(options)
    {

    }
    public DbSet<BuyOrder> BuyOrders { get; set; }
    public DbSet<SellOrder> SellOrders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SellOrder>().HasKey(sellOrder => sellOrder.SellOrderId);
        modelBuilder.Entity<SellOrder>().Property(p => p.StockSymbol).IsRequired();
        modelBuilder.Entity<SellOrder>().Property(p => p.StockName).IsRequired();
        modelBuilder.Entity<SellOrder>().Property(p => p.DateAndTimeOfOrder).IsRequired();
        modelBuilder.Entity<SellOrder>().Property(p => p.Quantity).IsRequired();
        modelBuilder.Entity<SellOrder>().Property(p => p.Price).IsRequired();


        modelBuilder.Entity<BuyOrder>().HasKey(buyOrder => buyOrder.BuyOrderId);

    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<string>().HaveMaxLength(100);
    }
}
