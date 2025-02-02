using _01_Framework.Infrastructure;

namespace ShopManagement.Configuration.Permissions
{
    public class ShopPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Product", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.ListProducts,"ListProducts"),
                        new PermissionDto(ShopPermissions.SearchProducts,"SearchProducts"),
                        new PermissionDto(ShopPermissions.CreateProducts,"CreateProducts"),
                        new PermissionDto(ShopPermissions.EditProduct,"EditProducts"),
                    }

                },
                {
                    "ProductCategory", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.SearchProductCategories,"SearchProductCategories"),
                        new PermissionDto(ShopPermissions.ListProductCategories,"ListProductCategories"),
                        new PermissionDto(ShopPermissions.CreateProductCategories,"CreateProductCategories"),
                        new PermissionDto(ShopPermissions.EditProductCategories,"EditProductCategories"),
                    }
                }
            };
        }
    }
}
