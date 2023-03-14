using ShoppingOnline.Models.Carts;

namespace ShoppingOnline.Models.Orders;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;
    
    private Cart _cart;
    
    public OrderRepository(AppDbContext context
        , Cart cart)
    {
        _context = context;
        _cart = cart;
    }
    
    public IQueryable<Order> Orders => _context.Orders;
    
    public void SaveOrder(Order order)
    {
        order.OrderPlaced = DateTime.Now;
        
        _context.Orders.Add(order);
        
        var cartItems = _cart.Items;

        foreach (var item in cartItems)
        {
            var orderDetail = new OrderDetail
            {
                Quantity = item.Quantity,
                Price = item.Product.Price,
                ProductId = item.Product.Id,
                OrderId = order.OrderId
            };
            
            _context.OrderDetails.Add(orderDetail);
        }
        _context.SaveChanges();
    }
}