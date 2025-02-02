using _01_Framework.Infrastructure;

namespace InventoryManagement.Infrastructure.Configuration.Permissions
{
    public class InventoryPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Inventory", new List<PermissionDto>
                    {
                        new PermissionDto(InventoryPermissions.ListInventories, "ListInventory"),
                        new PermissionDto(InventoryPermissions.SearchInventories, "SearchInventory"),
                        new PermissionDto(InventoryPermissions.CreateInventories, "CreateInventory"),
                        new PermissionDto(InventoryPermissions.EditInventories, "EditInventory"),
                        new PermissionDto(InventoryPermissions.Increase, "Increase"),
                        new PermissionDto(InventoryPermissions.Decrease, "Decrease"),
                        new PermissionDto(InventoryPermissions.OperationLog, "OperationLog"),
                    }

                }
            };
        }
    }
}
