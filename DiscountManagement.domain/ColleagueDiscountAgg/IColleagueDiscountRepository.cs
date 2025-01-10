using _01_Framework.Domain;
using DiscountManagement.Application.Contarct.ColleagueDiscount;

namespace DiscountManagement.domain.ColleagueDiscountAgg;

public interface IColleagueDiscountRepository : IRepository<ColleagueDiscount,long>
{
    EditColleagueDiscount GetDetails(long id);

    List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel);
}