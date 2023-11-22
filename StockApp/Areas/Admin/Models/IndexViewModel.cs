namespace StockApp.Areas.Admin.Models;

public class IndexViewModel
{
    public IEnumerable<UserViewModel> Users { get; }
    public PageViewModel PageViewModel { get; }
    public IndexViewModel(IEnumerable<UserViewModel> users, PageViewModel viewModel)
    {
        Users = users;
        PageViewModel = viewModel;
    }
}
