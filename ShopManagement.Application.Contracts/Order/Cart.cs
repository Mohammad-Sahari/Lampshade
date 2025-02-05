namespace ShopManagement.Application.Contracts.Order;

public class Cart
{
    public double TotalAmount => Items.Sum(x => x.TotalItemPrice);
    public double DiscountAmount => Items.Sum(x=>x.DiscountAmount);
    public double PayAmount => TotalAmount - DiscountAmount;
    public List<CartItem> Items { get; set; } = new();

    public void Add(CartItem cartItem)
    {
        Items.Add(cartItem);
    }
}