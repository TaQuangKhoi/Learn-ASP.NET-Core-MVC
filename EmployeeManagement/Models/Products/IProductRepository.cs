namespace CompanyManagement.Models.Products
{
    public interface IProductRepository
    {
        Product GetProduct(int ID);
        IEnumerable<Product> GetAllProducts();
    }
}
