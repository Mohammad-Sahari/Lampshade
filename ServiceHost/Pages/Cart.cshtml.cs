using _02_LampshadeQuery.Contract.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Query;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages
{
    public class CartModel : PageModel
    {
        public List<CartItem>? CartItems;
        public const string CookieName ="cart-items";
        private readonly IProductQuery _productQuery;

        public CartModel(IProductQuery productQuery)
        {
            CartItems = new List<CartItem>();
            _productQuery = productQuery;
        }

        public void OnGet()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(value);

            CartItems = _productQuery.CheckInventroyStatus(cartItems);
        }

        public IActionResult OnGetRemoveFromCart(long id)
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            Response.Cookies.Delete(CookieName);
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            var itemToRemove = cartItems.FirstOrDefault(x => x.Id == id);
            cartItems.Remove(itemToRemove);
            var options = new CookieOptions{Expires = DateTime.Now.AddDays(1)};
            Response.Cookies.Append(CookieName,serializer.Serialize(cartItems),options);
            return RedirectToPage("/Cart");
        }

        public IActionResult OnGetGoToCheckOut()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(value);

            CartItems = _productQuery.CheckInventroyStatus(cartItems);
            return RedirectToPage(CartItems.Any(x => !x.IsInStock) ? "/Cart" : "/Checkout");
        }
    }
}
