namespace StockApp.Areas.User.Models;

public class UserViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTimeOffset Created { get; set; }
    public int TotalOperationsCount { get; set; }
}
