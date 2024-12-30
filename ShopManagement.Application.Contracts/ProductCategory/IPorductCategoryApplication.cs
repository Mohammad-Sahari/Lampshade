using _01_Framework.Application;

namespace ShopManagement.Application.Contracts.ProductCategory
{
    public interface IPorductCategoryApplication
    {
        OperationResult Create(CreateProductCategory command);
        OperationResult Edit(EditProductCategory command);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);

        EditProductCategory GetDetails(long id);
    }
}
