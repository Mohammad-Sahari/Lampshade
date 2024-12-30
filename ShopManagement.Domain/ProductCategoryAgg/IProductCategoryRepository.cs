using _01_Framework.Domain;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository : IRepository<ProductCategory, long>
    {
        EditProductCategory GetDetails(long id);

        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}
