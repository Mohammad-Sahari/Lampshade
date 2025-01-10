namespace _02_LampshadeQuery.Contract.ProductCategory
{
    public interface IProductCategoryQuery
    {
        List<ProductCategoryQueryModel> GetProductCategories();
        //List<ProductCategoryQueryModel> GetProductCategoriesWithProducts();
        //ProductCategoryQueryModel GetProductCategoryWithProductsBy(string slug);
    }
}
