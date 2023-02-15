namespace CompanyManagement.Models.Products
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> _productList;
        public ProductRepository()
        {
            _productList = new List<Product>
            {
                new Product() { ID = 1, Name = "Laptop", Description = "Laptop Dell", Price = 1000, Category = "IT" },
                new Product() { ID = 2, Name = "PC", Description = "PC ASUS", Price = 2000, Category = "IT" }
            };
        } 
        IEnumerable<Product> IProductRepository.GetAllProducts()
        {
            return _productList;
        }

        Product IProductRepository.GetProduct(int ID)
        {
            return _productList.FirstOrDefault(e => e.ID == ID);  
        }
    }
}
