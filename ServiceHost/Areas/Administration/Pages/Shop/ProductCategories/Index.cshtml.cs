using _01_Framework.Application;
using _01_Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
    [Authorize(Roles = Roles.Administrator)]
    public class IndexModel : PageModel
    {
        public List<ProductCategoryViewModel> ProductCategories;
        public ProductCategorySearchModel searchModel;
        private readonly IPorductCategoryApplication _productCategoryApplication;

        public IndexModel(IPorductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }
        [NeedsPermission(ShopPermissions.ListProductCategories)]
        public void OnGet(ProductCategorySearchModel searchModel)
        {
            ProductCategories = _productCategoryApplication.Search(searchModel);
        }
        [NeedsPermission(ShopPermissions.CreateProductCategories)]
        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateProductCategory());
        }

        public JsonResult OnPostCreate(CreateProductCategory command)
        {
           var result = _productCategoryApplication.Create(command);
           return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var productCategory = _productCategoryApplication.GetDetails(id);
            return Partial("Edit", productCategory);
        }
        [NeedsPermission(ShopPermissions.EditProductCategories)]

        public JsonResult OnPostEdit(EditProductCategory command)
        {
            //if (ModelState.IsValid)
            //{
                
            //}
            var result = _productCategoryApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
