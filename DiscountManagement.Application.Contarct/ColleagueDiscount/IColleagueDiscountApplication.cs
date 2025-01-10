using _01_Framework.Application;

namespace DiscountManagement.Application.Contarct.ColleagueDiscount;

public interface IColleagueDiscountApplication
{
    OperationResult Define(DefineColleagueDiscount command);
    List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel);
    OperationResult Edit(EditColleagueDiscount command);
    EditColleagueDiscount GetDetails(long id);
    OperationResult Remove(long id);
    OperationResult Restore(long id);
}