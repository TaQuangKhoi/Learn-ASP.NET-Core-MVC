namespace ShoppingOnline.Models;

public class Cart
{
    private List<CartItem> ItemCollection = new List<CartItem>();
    public virtual IEnumerable<CartItem> Items => ItemCollection;

    public virtual void AddItem(Product product, int quanity)
    {
        CartItem Item = ItemCollection.Where(p => p.Product.Id == product.Id).FirstOrDefault();
        if (Item == null)
        {
            ItemCollection.Add(new CartItem
            {
                Product = product,
                Quantity = quanity
            });
        }
        else
        {
            Item.Quantity += quanity;
        }
    }

    public virtual void RemoveItem(Product product) =>
        ItemCollection.RemoveAll(l => l.Product.Id == product.Id);

    public virtual decimal ComputeTotalValue() => ItemCollection.Sum(l => l.Product.Price * l.Quantity);

    public virtual void Clear() => ItemCollection.Clear();
}

public class CartItem
{
    public int CartItemId { get; set; }

    public Product Product { get; set; }

    public int? Quantity { get; set; }
}