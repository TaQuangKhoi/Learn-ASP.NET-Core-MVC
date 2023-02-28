namespace CompanyManagement.Models.ViewModels;

public class CreateViewModel
{
    public string Name { get; set; }
    public string Department { get; set; }
    public IFormFile Photo { get; set; }
}