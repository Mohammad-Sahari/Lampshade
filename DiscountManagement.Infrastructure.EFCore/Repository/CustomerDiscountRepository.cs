using _01_Framework.Application;
using _01_Framework.Infrastructure;
using DiscountManagement.Application.Contarct.CustomerDiscount;
using DiscountManagement.domain.CustomerDiscountAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class CustomerDiscountRepository : RepositoryBase<CustomerDiscount, long>, ICustomerDiscountRepository
    {
        private readonly DiscountContext _context;
        private readonly ShopContext _shopContext;
        public CustomerDiscountRepository(DiscountContext context, ShopContext shopContext) : base(context)
        {
            _shopContext = shopContext;
            _context = context;
        }


        public EditCustomerDiscount GetDetails(long id)
        {
            return _context.CustomerDiscounts.Select(x => new EditCustomerDiscount
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                EndDate = x.EndDate.ToString(),
                ProductId = x.ProductId,
                StartDate = x.StartDate.ToString(),
                EventName = x.EventName
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var query = _context.CustomerDiscounts.Select(customerDiscount => new CustomerDiscountViewModel
            {
                Id = customerDiscount.Id,
                DiscountRate = customerDiscount.DiscountRate,
                EndDate = customerDiscount.EndDate.ToFarsi(),
                EndDateGr = customerDiscount.EndDate,
                ProductId = customerDiscount.ProductId,
                StartDate = customerDiscount.StartDate.ToFarsi(),
                StartDateGr = customerDiscount.StartDate,
                EventName = customerDiscount.EventName,
                CreationDate = customerDiscount.CreationDate.ToFarsi()
            });

            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            if (!string.IsNullOrWhiteSpace(searchModel.StartDate))
            {
                query = query.Where(x => x.StartDateGr > searchModel.StartDate.ToGeorgianDateTime());
            }

            if (!string.IsNullOrWhiteSpace(searchModel.EndDate))
            {
                query = query.Where(x => x.EndDateGr < searchModel.EndDate.ToGeorgianDateTime());
            }

            var discounts = query.OrderByDescending(x => x.Id).ToList();
            discounts.ForEach(discount => discount.Product = products.FirstOrDefault(x=> x.Id == discount.ProductId)?.Name);
            return discounts;
        }
    }
}
