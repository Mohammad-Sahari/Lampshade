using _01_Framework.Application;

namespace ShopManagement.Application.Contracts.ProductCategory
{
    public interface IPorductCategoryApplication
    {
        OperationResult Create(CreateProductCategory command);
        OperationResult Edit(EditProductCategory command);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
        List<ProductCategoryViewModel> GetProductCategories();
        EditProductCategory GetDetails(long id);
    }
}
