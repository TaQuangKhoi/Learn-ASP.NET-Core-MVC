namespace ShoppingOnline.Models;

public class ProductRepository : IProductInterface
{
    private readonly AppDbContext _appDbContext;
    
    public ProductRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public Product GetProduct(int id)
    {
        Product? product = _appDbContext.Products.Find(id);
        if (product == null)
        {
            throw new Exception("Product not found");
        }
        return product;
    }

    public Product AddProduct(Product product)
    {
        _appDbContext.Products.Add(product);
        _appDbContext.SaveChanges();
        return product;
    }

    public IEnumerable<Product> GetAllProducts()
    {
        return _appDbContext.Products;
    }

    public Product UpdateProduct(Product product)
    {
        _appDbContext.Products.Update(product);
        _appDbContext.SaveChanges();
        return product;
    }

    public Product DeleteProduct(int id)
    {
        Product? product = _appDbContext.Products.Find(id);
        if (product != null)
        {
            _appDbContext.Products.Remove(product);
            _appDbContext.SaveChanges();
            
        }
        else
        {
            throw new Exception("Product not found");
        }
        return product;
    }
}