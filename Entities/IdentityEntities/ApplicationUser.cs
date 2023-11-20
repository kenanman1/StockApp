using Microsoft.AspNetCore.Identity;
using StockApp.Model;
using System.ComponentModel.DataAnnotations;

namespace Entities.IdentityEntities;

public class ApplicationUser : IdentityUser
{
    [StringLength(200)]
    public string FirstName { get; set; }

    [StringLength(200)]
    public string LastName { get; set; }

    public int TotalOperationsCount { get; set; }

    public DateTimeOffset Created { get; set; }

    public virtual ICollection<BuyOrder> BuyOrders { get; set; }
    public virtual ICollection<SellOrder> SellOrders { get; set; }
}
