using _01_Framework.Application;
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
                CategoryId = x.CategoryId,
                CreationDate = x.CreationDate.ToFarsi()
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
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
                ShortDescription = x.ShortDescription,
            }).FirstOrDefault(x => x.Id == id);
        }

        public Product GetProductWithCategory(long id)
        {
            return _context.Products
                .Include(x => x.Category)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
