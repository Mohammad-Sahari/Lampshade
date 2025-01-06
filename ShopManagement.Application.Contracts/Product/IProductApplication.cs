using _01_Framework.Application;

namespace ShopManagement.Application.Contracts.Product
{
    public interface IProductApplication
    {
        OperationResult Create(CreateProduct command);
        OperationResult Edit(EditProduct command);
        EditProduct GetDetails(long id);
        List<ProductViewModel> Search(ProductSearchModel searchModel);
        OperationResult InStock(long id);
        OperationResult NotAvailable(long id);
        List<ProductViewModel> GetProducts();
    }
}
