namespace ShoppingOnline.Models.Orders;

public interface IOrderRepository
{
    IQueryable<Order> Orders { get; }
    void SaveOrder(Order order);
}