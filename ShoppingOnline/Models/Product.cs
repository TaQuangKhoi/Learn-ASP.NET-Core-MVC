using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ShoppingOnline.Models;

public class Product
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Please enter a product name")]
    public string? Name { get; set; }
    
    [Required(ErrorMessage = "Please enter a short description")]
    public string? ShortDescription { get; set; }
    
    [Required(ErrorMessage = "Please enter a long description")]
    public string? LongDescription { get; set; }
    
    [Required(ErrorMessage = "Please enter a category")]
    public string? Category { get; set; }
    
    [Required(ErrorMessage = "Please enter a price")]
    public float Price { get; set; }
    
    [Required(ErrorMessage = "Please enter a stock")]
    public string? Stock { get; set; }
    
    public string ImageUrl { get; set; }
    
    public string toString()
    {
        return "Id: " + Id + ", Name: " + Name + ", ShortDescription: " + ShortDescription + ", LongDescription: " + LongDescription + ", Category: " + Category + ", Price: " + Price + ", Stock: " + Stock + ", ImageUrl: " + ImageUrl;
    }
}