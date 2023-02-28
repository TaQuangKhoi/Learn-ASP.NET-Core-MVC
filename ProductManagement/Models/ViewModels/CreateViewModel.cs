namespace ProductManager.Models.ViewModels;

public class CreateViewModel
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public string Price { get; set; }
    public IFormFile Photo { get; set; }
}