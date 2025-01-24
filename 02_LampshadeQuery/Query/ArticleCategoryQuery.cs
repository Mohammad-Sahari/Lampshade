using _01_Framework.Application;
using _02_LampshadeQuery.Contract.Article;
using _02_LampshadeQuery.Contract.ArticleCategory;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace _02_LampshadeQuery.Query
{
    public class ArticleCategoryQuery : IArticleCategoryQuery
    {
        private readonly BlogContext _context;

        public ArticleCategoryQuery(BlogContext context)
        {
            _context = context;
        }

        public ArticleCategoryQueryModel GetArticleCategory(string slug)
        {
            return _context.ArticleCategories.Select(x => new ArticleCategoryQueryModel
            {
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Description = x.Description,
                DisplayOrder = x.DisplayOrder,
                CanonicalAddress = x.CanonicalAddress,
                Slug = x.Slug,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                ArticleCount = x.Articles.Count,
                Articles = MapArticles(x.Articles)
            }).FirstOrDefault(x=>x.Slug == slug);
        }

        private static List<ArticleQueryModel> MapArticles(List<Article> articles)
        {
            return articles.Select(x => new ArticleQueryModel
            {
                Title = x.Title,
                ShortDescription = x.ShortDescription,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
                PublishDate = x.PublishDate.ToFarsi(),
            }).ToList();
        }

        public List<ArticleCategoryQueryModel> GetArticleCategories()
        {
            return _context.ArticleCategories
                .Include(x => x.Articles)
                .Select(x => new ArticleCategoryQueryModel
                {
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Description = x.Description,
                    DisplayOrder = x.DisplayOrder,
                    Slug = x.Slug,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    ArticleCount = x.Articles.Count
                }).ToList();
        }
    }
}
