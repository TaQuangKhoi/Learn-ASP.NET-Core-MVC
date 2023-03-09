namespace ShoppingOnline.Models.ViewModals;

public class ProductListViewModel
{
    public IEnumerable<Product> Products { get; set;}
    
    public string CurrentCategory { get; set;}
}