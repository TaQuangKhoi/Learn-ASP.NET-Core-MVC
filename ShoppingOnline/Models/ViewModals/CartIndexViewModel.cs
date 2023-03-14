using ShoppingOnline.Models.Carts;

namespace ShoppingOnline.Models.ViewModals;

public class CartIndexViewModel
{
    public Cart Cart { get; set; }
    
    public string ReturnUrl { get; set;}
}