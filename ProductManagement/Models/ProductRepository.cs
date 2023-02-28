namespace ProductManagement.Models.Products
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> _productList;
        public ProductRepository()
        {

        } 
        IEnumerable<Product> IProductRepository.GetAllProducts()
        {
            return _productList;
        }

        public Product Add(Product product)
        {
            throw new NotImplementedException();
        }

        public Product Update(Product product)
        {
            throw new NotImplementedException();
        }

        public Product Delete(Product product)
        {
            throw new NotImplementedException();
        }

        Product IProductRepository.GetProduct(int ID)
        {
            return _productList.FirstOrDefault(e => e.ID == ID);  
        }
    }
}
