namespace ProductManagement.Models.Products
{
    public interface IProductRepository
    {
        Product GetProduct(int ID);
        IEnumerable<Product> GetAllProducts();
        
        Product Add(Product product);
        
        Product Update(Product product);
        
        Product Delete(Product product);
    }
}
