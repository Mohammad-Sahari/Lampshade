using ShopManagement.Application.Contracts.Order;

namespace _02_LampshadeQuery.Contract
{
    public interface ICartCalculatorService
    {
        Cart ComputeCart(List<CartItem> cartItems);
    }
}
