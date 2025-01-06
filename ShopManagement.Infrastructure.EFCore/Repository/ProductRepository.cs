using _01_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductRepository : RepositoryBase<Product, long> , IProductRepository
    {
        private readonly ShopContext _context;

        public ProductRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public List<ProductViewModel> Search(ProductSearchModel command)
        {
            var query = _context.Products
                .Include(x => x.Category)
                .Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Category = x.Category.Name,
                Code = x.Code,
                Picture = x.Picture,
                UnitPrice = x.UnitPrice,
                CategoryId = x.CategoryId,
                IsInStock = x.IsInStock,
                CreationDate = x.CreationDate.ToString()
            });
            if (!string.IsNullOrWhiteSpace(command.Name))
                query = query.Where(x => x.Name.Contains(command.Name));
            
            if (!string.IsNullOrWhiteSpace(command.Code))
                query = query.Where(x => x.Code.Contains(command.Code));

            if (command.CategoryId != 0)
                query = query.Where(x => x.CategoryId == command.CategoryId);

            return query.OrderByDescending(x => x.Id).ToList(); 

        }

        public List<ProductViewModel> GetProducts()
        {
            return _context.Products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        }

        public EditProduct GetDetails(long id)
        {
            return _context.Products.Select(x => new EditProduct
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                CategoryId = x.CategoryId,
                Description = x.Description,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
                ShortDescription = x.ShortDescription,
                UnitPrice = x.UnitPrice
            }).FirstOrDefault(x => x.Id == id);
        }
    }
}
