using System.Linq.Expressions;
using _01_Framework.Application;
using _02_LampshadeQuery.Contract;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Infrastructure.EFCore;

namespace _02_LampshadeQuery.Query
{
    public class CartCalculateService : ICartCalculatorService
    {
        private readonly DiscountContext _discountContext;
        private readonly IAuthHelper _authHelper;
        public CartCalculateService(DiscountContext discountContext, IAuthHelper authHelper)
        {
            _discountContext = discountContext;
            _authHelper = authHelper;
        }

        public Cart ComputeCart(List<CartItem> cartItems)
        {
            var cart = new Cart();
            var colleagueDiscounts = _discountContext.ColleagueDiscounts
                .Where(x => x.IsRemoved == false)
                .ToDictionary(x => x.ProductId, x => x.DiscountRate);

            var customerDiscount = _discountContext.CustomerDiscounts
                .Where(x =>x.StartDate<DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate })
                .ToDictionary(x => x.ProductId, x => x.DiscountRate);

            var currentAccountRole = _authHelper.CurrentAccountRole();
            if (currentAccountRole == Roles.ColleagueUser)
            {
                foreach (var cartItem in cartItems)
                {
                    if (colleagueDiscounts.TryGetValue(cartItem.Id, out var discountRate))
                    {
                        cartItem.DiscountRate = discountRate;
                    }
                    cart.Add(cartItem);
                }
            }
            else
            {
                foreach (var cartItem in cartItems)
                {
                    if (customerDiscount.TryGetValue(cartItem.Id, out var discountRate))
                    {
                        cartItem.DiscountRate = discountRate;
                    }
                    cart.Add(cartItem);
                }
            }

            return cart;
        }
    }
}
