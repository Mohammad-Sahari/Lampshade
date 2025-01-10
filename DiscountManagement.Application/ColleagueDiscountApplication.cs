using _01_Framework.Application;
using DiscountManagement.Application.Contarct.ColleagueDiscount;
using DiscountManagement.domain.ColleagueDiscountAgg;

namespace DiscountManagement.Application
{
    public class ColleagueDiscountApplication : IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository _repository;

        public ColleagueDiscountApplication(IColleagueDiscountRepository repository)
        {
            _repository = repository;
        }

        public OperationResult Define(DefineColleagueDiscount command)
        {
            var operation = new OperationResult();
            if (_repository.Exists(x => x.ProductId == command.ProductId
            && x.DiscountRate == command.DiscountRate))
                return operation.Failed(ApplicationMessages.DuplicateRecord);

            var colleagueDiscount = new ColleagueDiscount(command.ProductId, command.DiscountRate);
            _repository.Create(colleagueDiscount);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }

        public OperationResult Edit(EditColleagueDiscount command)
        {
            var operation = new OperationResult();
            var colleagueDiscount = _repository.Get(command.Id);
            if (colleagueDiscount is null)
                return operation.Failed(ApplicationMessages.NotFound);

            if (_repository.Exists(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicateRecord);

            colleagueDiscount.Edit(command.ProductId, command.DiscountRate);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var colleagueDiscount = _repository.Get(id);
            if (colleagueDiscount is null)
                return operation.Failed(ApplicationMessages.NotFound);

            colleagueDiscount.Remove();
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var colleagueDiscount = _repository.Get(id);
            if (colleagueDiscount is null)
                return operation.Failed(ApplicationMessages.NotFound);

            colleagueDiscount.Restore();
            _repository.SaveChanges();
            return operation.Succeeded();
        }
    }
}
