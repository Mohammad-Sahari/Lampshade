using _02_LampshadeQuery.Contract.Product;

namespace _02_LampshadeQuery.Contract.ProductCategory
{
    public interface IProductCategoryQuery
    {
        ProductCategoryQueryModel GetProductCategoryWithProductsBy(string slug);
        List<ProductCategoryQueryModel> GetProductCategories();
        List<ProductCategoryQueryModel> GetProductCategoriesWithProducts();
    }
}
