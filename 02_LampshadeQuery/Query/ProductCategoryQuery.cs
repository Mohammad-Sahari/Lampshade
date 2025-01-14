using _01_Framework.Application;
using _02_LampshadeQuery.Contract.Product;
using _02_LampshadeQuery.Contract.ProductCategory;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EFCore;

namespace _02_LampshadeQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _context;
        private readonly InventoryContext _inventoryContext;

        public ProductCategoryQuery(ShopContext context, InventoryContext inventoryContext)
        {
            _context = context;
            _inventoryContext = inventoryContext;
        }

        public List<ProductCategoryQueryModel> GetProductCategories()
        {
            return _context.ProductCategories.Select(x => new ProductCategoryQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug
            }).ToList();
        }

        public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
        {
            var inventory = _inventoryContext.Inventory
                .Select(x => new { x.ProductId,x.UnitPrice }).ToList();

            var categories = _context.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Products = MapProducts(x.Products)
                }).ToList();
            foreach (var category in categories)
            {
                foreach (var product in category.Products)
                {
                    product.Price = inventory
                        .FirstOrDefault(x => x.ProductId == product.Id)
                        ?.UnitPrice.ToMoney();
                }
            }

            return categories;
        }

        private static List<ProductQueryModel> MapProducts(List<Product> products)
        {
            return products.Select(product => new ProductQueryModel
            {
                Id = product.Id,
                Category = product.Category.Name,
                Name = product.Name,
                Picture = product.Picture,
                PictureAlt = product.PictureAlt,
                PictureTitle = product.PictureTitle,
                Slug = product.Slug

            }).ToList();
        }
    }
}
