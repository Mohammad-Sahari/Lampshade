using _01_Framework.Domain;
using DiscountManagement.Application.Contarct.CustomerDiscount;

namespace DiscountManagement.domain.CustomerDiscountAgg
{
    public interface ICustomerDiscountRepository : IRepository<CustomerDiscount, long>
    {
        EditCustomerDiscount GetDetails(long id);
        List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel);
    }
}
