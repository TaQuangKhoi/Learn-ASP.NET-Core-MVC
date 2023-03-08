namespace ShoppingOnline.Models;

public interface IProductInterface
{
    Product GetProduct(int id);
    Product AddProduct(Product product);
    IEnumerable<Product> GetAllProducts();
    Product UpdateProduct(Product product);
    Product DeleteProduct(int id);
}