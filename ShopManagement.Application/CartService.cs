using ShopManagement.Application.Contracts.Order;

namespace ShopManagement.Application
{
    public class CartService : ICartService
    {
        public Cart Cart { get; set; }
        public void Set(Cart cart)
        {
            this.Cart = cart;
        }

        public Cart Get()
        {
            return this.Cart;
        }
    }
}
