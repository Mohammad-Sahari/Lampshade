namespace _02_LampshadeQuery.Contract.Article
{
    public interface IArticleQuery
    {
        ArticleQueryModel GetArticleDetails(string slug);
        List<ArticleQueryModel> LatestArticles();
    }
}
