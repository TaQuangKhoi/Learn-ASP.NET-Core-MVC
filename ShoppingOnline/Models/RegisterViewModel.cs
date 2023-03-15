using System.ComponentModel.DataAnnotations;

namespace ShoppingOnline.Models;

public class RegisterViewModal
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}