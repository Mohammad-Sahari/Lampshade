using _02_LampshadeQuery.Contract.ArticleCategory;
using _02_LampshadeQuery.Contract.ProductCategory;

namespace _02_LampshadeQuery
{
    public class MenuModel
    {
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
        public List<ProductCategoryQueryModel> ProductCategories { get; set; }
    }
}
