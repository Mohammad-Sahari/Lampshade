using _01_Framework.Domain;
using BlogManagement.Application.Contracts.Article;

namespace BlogManagement.Domain.ArticleAgg
{
    public interface IArticleReposiotry : IRepository<Article,long>
    {
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
        EditArticle GetDetails(long id);
        Article GetWithCategory(long id);
    }
}
