using System.ComponentModel.DataAnnotations;

namespace ShoppingOnline.Models;

public class Product
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Please enter a product name")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Please enter a short description")]
    public string ShortDescription { get; set; }
    
    [Required(ErrorMessage = "Please enter a long description")]
    public string LongDescription { get; set; }
    
    [Required(ErrorMessage = "Please enter a category")]
    public string Category { get; set; }
    
    [Required(ErrorMessage = "Please enter a price")]
    public decimal Price { get; set; }
    
    [Required(ErrorMessage = "Please enter a stock")]
    public string Stock { get; set; }
    
}