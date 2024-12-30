using System.Linq.Expressions;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository
    {
        void Add(ProductCategory entity);
        ProductCategory Get(long id);

        List<ProductCategory> GetList();
        bool Exists(Expression<Func<ProductCategory, bool>> expression);
        void SaveChanges();
        EditProductCategory GetDetails(long id);

        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}
