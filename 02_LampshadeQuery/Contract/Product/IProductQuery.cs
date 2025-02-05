using ShopManagement.Application.Contracts.Order;

namespace _02_LampshadeQuery.Contract.Product
{
    public interface IProductQuery
    {
        List<ProductQueryModel> GetLatestProducts();
        List<ProductQueryModel> Search(string value);
        ProductQueryModel GetProductDetails(string slug);
        List<CartItem> CheckInventroyStatus(List<CartItem> cartItems);
    }
}
