using Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockApp.Model;

namespace StockApp.DataBase;

public class StockDbContext : IdentityDbContext<ApplicationUser>
{
    public StockDbContext(DbContextOptions<StockDbContext> options) : base(options)
    {

    }
    public DbSet<BuyOrder> BuyOrders { get; set; }
    public DbSet<SellOrder> SellOrders { get; set; }
    public DbSet<ApplicationUser> AppUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var roleAdmin = new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" };
        var roleUser = new IdentityRole { Name = "User", NormalizedName = "USER" };
        modelBuilder.Entity<IdentityRole>().HasData(roleAdmin, roleUser);

        modelBuilder.Entity<SellOrder>().HasKey(sellOrder => sellOrder.SellOrderId);
        modelBuilder.Entity<SellOrder>().Property(p => p.StockSymbol).IsRequired();
        modelBuilder.Entity<SellOrder>().Property(p => p.StockName).IsRequired();
        modelBuilder.Entity<SellOrder>().Property(p => p.DateAndTimeOfOrder).IsRequired();
        modelBuilder.Entity<SellOrder>().Property(p => p.Quantity).IsRequired();
        modelBuilder.Entity<SellOrder>().Property(p => p.Price).IsRequired();
        modelBuilder.Entity<SellOrder>().Property(p => p.StockSymbol).HasMaxLength(50);
        modelBuilder.Entity<SellOrder>().Property(p => p.StockName).HasMaxLength(50);
        modelBuilder.Entity<SellOrder>().HasOne(s => s.ApplicationUser).WithMany(u => u.SellOrders).HasForeignKey(s => s.UserId);

        modelBuilder.Entity<BuyOrder>().HasKey(buyOrder => buyOrder.BuyOrderId);
        modelBuilder.Entity<BuyOrder>().Property(p => p.StockSymbol).HasMaxLength(50);
        modelBuilder.Entity<BuyOrder>().Property(p => p.StockName).HasMaxLength(50);
        modelBuilder.Entity<BuyOrder>().HasOne(b => b.ApplicationUser).WithMany(u => u.BuyOrders).HasForeignKey(b => b.UserId);

        base.OnModelCreating(modelBuilder);
    }
}
