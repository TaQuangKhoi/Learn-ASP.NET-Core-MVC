using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ShoppingOnline.Models.Orders;

public class Order
{
    [BindNever] public int OrderId { get; set; }
    [BindNever] public IQueryable<OrderDetail> DetailItems { get; set; }
    public bool Shipped { get; set; }

    [Required(ErrorMessage = "Please enter a name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Please enter the first address")]
    public string Address1 { get; set; }

    public string Address2 { get; set; }

    [Required(ErrorMessage = "Please enter a city name")]
    public string City { get; set; }

    public string Zip { get; set; }
    public DateTime OrderPlaced { get; set; }
}