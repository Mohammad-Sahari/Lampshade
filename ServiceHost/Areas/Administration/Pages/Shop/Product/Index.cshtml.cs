using System.Dynamic;
using _01_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Configuration.Permissions;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ServiceHost.Areas.Administration.Pages.Shop.Product
{
    public class IndexModel : PageModel
    {
        [TempData] public string Message { get; set; }

        public List<ProductViewModel> Products;
        public ProductSearchModel searchModel;
        public SelectList ProductCategories;

        private readonly IProductApplication _productApplication;
        private readonly IPorductCategoryApplication _productCategoryApplication;

        public IndexModel(IProductApplication productApplication, IPorductCategoryApplication porductCategoryApplication)
        {
            _productApplication = productApplication;
            _productCategoryApplication = porductCategoryApplication;
        }
        [NeedsPermission(ShopPermissions.ListProducts)]
        public void OnGet(ProductSearchModel searchModel)
        {
            Products = _productApplication.Search(searchModel);
            ProductCategories = new SelectList(_productCategoryApplication.GetProductCategories(),"Id","Name" );
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateProduct
            {
                Categories = _productCategoryApplication.GetProductCategories()
            };
            return Partial("./Create", command);
        }
        [NeedsPermission(ShopPermissions.CreateProducts)]
        public JsonResult OnPostCreate(CreateProduct command)
        {
           var result = _productApplication.Create(command);
           return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var product = _productApplication.GetDetails(id);
            product.Categories = _productCategoryApplication.GetProductCategories();
            return Partial("Edit", product);
        }
        [NeedsPermission(ShopPermissions.EditProduct)]
        public JsonResult OnPostEdit(EditProduct command)
        {
            var result = _productApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
