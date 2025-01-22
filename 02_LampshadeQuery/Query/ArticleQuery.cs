using _01_Framework.Application;
using _02_LampshadeQuery.Contract.Article;
using BlogManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace _02_LampshadeQuery.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogContext _context;

        public ArticleQuery(BlogContext context)
        {
            _context = context;
        }

        public List<ArticleQueryModel> LatestArticles()
        {

            return _context.Articles.Where(x => x.PublishDate <= DateTime.Now)
                .Include(x=>x.ArticleCategory)
                .Select(x => new ArticleQueryModel
            {
                Title= x.Title,
                ShortDescription= x.ShortDescription,
                Description= x.Description,
                PublishDate= x.PublishDate.ToFarsi(),
                PublishMonth = x.PublishDate.ToFarsi().ExtractMonth(),
                PublishDay = x.PublishDate.ToFarsi().ExtractDay(),
                Picture= x.Picture,
                PictureAlt= x.PictureAlt,
                PictureTitle= x.PictureTitle,
                Slug= x.Slug,
                MetaDescriptio= x.MetaDescriptio,
                Keywords= x.Keywords,
                CanonicalAddress= x.CanonicalAddress,
                CategoryId= x.CategoryId,
                CategoryName= x.ArticleCategory.Name,
                CategorySlug= x.ArticleCategory.Slug
            }).ToList();
        }
    }
}
