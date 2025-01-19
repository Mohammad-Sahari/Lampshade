using _01_Framework.Domain;
using ShopManagement.Application.Contracts.Comment;

namespace ShopManagement.Domain.CommentAgg
{
    public interface ICommentRepository : IRepository<Comment,long>
    {
        List<CommentViewModel> Search(CommentSearchModel searchModel);

    }
}
