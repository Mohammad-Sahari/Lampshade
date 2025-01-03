using _01_Framework.Domain;
using ShopManagement.Application.Contracts.Product;

namespace ShopManagement.Domain.ProductAgg
{
    public interface IProductRepository : IRepository<Product, long>
    {
        List<ProductViewModel> Search(ProductSearchModel command);

        EditProduct GetDetails(long id);
    }
}
