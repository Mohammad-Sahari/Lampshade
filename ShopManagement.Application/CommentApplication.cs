using _01_Framework.Application;
using ShopManagement.Application.Contracts.Comment;
using ShopManagement.Domain.CommentAgg;

namespace ShopManagement.Application
{
    public class CommentApplication : ICommentApplication
    {
        private readonly ICommentRepository _repository;

        public CommentApplication(ICommentRepository repository)
        {
            _repository = repository;
        }

        public OperationResult AddComment(AddComment command)
        {
            var operation = new OperationResult();

            var comment = new Comment(command.Name, command.Email, command.Message, command.ProductId);
            _repository.Create(comment);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Confirm(long id)
        {
            var operation = new OperationResult();
            var comment = _repository.Get(id);
            if(comment is null)
                return operation.Failed(ApplicationMessages.NotFound);

            comment.Confirm();
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Cancel(long id)
        {
            var operation = new OperationResult();
            var comment = _repository.Get(id);
            if (comment is null)
                return operation.Failed(ApplicationMessages.NotFound);

            comment.Cancel();
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }
    }
}
