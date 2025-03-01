using _01_Framework.Application;
using _01_Framework.Infrastructure;
using DiscountManagement.Application.Contarct.ColleagueDiscount;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    [Authorize(Roles = Roles.Administrator)]

public class IndexModel : PageModel
    {
        [TempData] public string Message { get; set; }

        public List<InventoryViewModel> Inventory;
        public InventorySearchModel searchModel;
        public SelectList Products;

        private readonly IProductApplication _productApplication;
        private readonly IInventoryApplication _inventoryApplication;

        public IndexModel(IProductApplication productApplication,
            IInventoryApplication inventoryApplication)
        {
            _productApplication = productApplication;
            _inventoryApplication = inventoryApplication;
        }
        [NeedsPermission(InventoryPermissions.ListInventories)]
        public void OnGet(InventorySearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            Inventory = _inventoryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateInventory()
            {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Create", command);
        }
        [NeedsPermission(InventoryPermissions.CreateInventories)]

        public JsonResult OnPostCreate(CreateInventory command)
        {
            var result = _inventoryApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var Inventory = _inventoryApplication.GetDetails(id);
            Inventory.Products = _productApplication.GetProducts();
            return Partial("Edit", Inventory);
        }
        [NeedsPermission(InventoryPermissions.EditInventories)]

        public JsonResult OnPostEdit(EditInventory command)
        {
            var result = _inventoryApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetIncrease(long id)
        {
            var command = new IncreaseInventory
            {
                InventoryId = id
            };
            return Partial("Increase", command);
        }
        [NeedsPermission(InventoryPermissions.Increase)]

        public JsonResult OnPostIncrease(IncreaseInventory command)
        {
            var result = _inventoryApplication.Increase(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetDecrease(long id)
        {
            var command = new DecreaseInventory()
            {
                InventoryId = id
            };
            return Partial("Decrease", command);
        }
        [NeedsPermission(InventoryPermissions.Decrease)]

        public JsonResult OnPostDecrease(DecreaseInventory command)
        {
            var result = _inventoryApplication.Decrease(command);
            return new JsonResult(result);
        }
        [NeedsPermission(InventoryPermissions.OperationLog)]

        public IActionResult OnGetOperationLog(long id)
        {
            var log = _inventoryApplication.GetOperationLog(id);
            return Partial("OperationLog", log);
        }
    }
}
