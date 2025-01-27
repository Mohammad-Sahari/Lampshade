using _01_Framework.Application;
using _02_LampshadeQuery.Contract.Article;
using _02_LampshadeQuery.Contract.Comment;
using BlogManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace _02_LampshadeQuery.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogContext _context;
        private readonly CommentContext _commentContext;
        public ArticleQuery(BlogContext context, CommentContext commentContext)
        {
            _context = context;
            _commentContext = commentContext;
        }

        public ArticleQueryModel GetArticleDetails(string slug)
        {
            var article = _context.Articles
                .Include(x=>x.ArticleCategory)
                .Where(x => x.PublishDate <= DateTime.Now)
                .Select(x => new ArticleQueryModel
            {
                Id = x.Id,
                Title = x.Title,
                ShortDescription = x.ShortDescription,
                Description = x.Description,
                PublishDate = x.PublishDate.ToFarsi(),
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
                MetaDescriptio = x.MetaDescriptio,
                Keywords = x.Keywords,
                CanonicalAddress = x.CanonicalAddress,
                CategoryId = x.CategoryId,
                CategoryName = x.ArticleCategory.Name,
                CategorySlug = x.ArticleCategory.Slug,

            }).FirstOrDefault(x=>x.Slug == slug);

            if (!string.IsNullOrWhiteSpace(article.Keywords))
                article.KeywordList = article.Keywords?.Split(" ").ToList();

            var Comments = _commentContext.Comments
                .Where(x => x.Type == CommentType.Article)
                .Where(x => x.OwnerRecordId == article.Id)
                .Where(x => x.IsConfirmed && !x.IsCanceled)
                .Select(x => new CommentQueryModel
                {
                    Id = x.Id,
                    Message = x.Message,
                    Name = x.Name,
                    ParentId = x.ParentId,
                    CreationDate = x.CreationDate.ToFarsi(),
                }).OrderByDescending(x => x.Id).ToList();

            foreach (var comment in Comments)
            {
                if (comment.ParentId > 0)
                    comment.ParentName = Comments.FirstOrDefault(x => x.Id == comment.ParentId)?.Name;
            }

            article.Comments = Comments;
            return article;
        }

        public List<ArticleQueryModel> LatestArticles()
        {

            return _context.Articles.Where(x => x.PublishDate <= DateTime.Now)
                .Include(x => x.ArticleCategory)
                .Select(x => new ArticleQueryModel
                {
                    Title = x.Title,
                    ShortDescription = x.ShortDescription,
                    PublishDate = x.PublishDate.ToFarsi(),
                    PublishMonth = x.PublishDate.ToFarsi().ExtractMonth(),
                    PublishDay = x.PublishDate.ToFarsi().ExtractDay(),
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    CategoryId = x.CategoryId,
                }).ToList();
        }
    }
}
