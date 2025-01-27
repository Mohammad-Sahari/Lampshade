using _01_Framework.Domain;
using CommentManagement.Application.Contracts.Comment;

namespace CommentManagement.Domain.CommentAgg
{
    public interface ICommentRepository : IRepository<Comment, long>
    {
        List<CommentViewModel> Search(CommentSearchModel searchModel);

    }
}
