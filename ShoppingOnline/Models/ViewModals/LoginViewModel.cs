using System.ComponentModel.DataAnnotations;

namespace ShoppingOnline.Models.ViewModals;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}