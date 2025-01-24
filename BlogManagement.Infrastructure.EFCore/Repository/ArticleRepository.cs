using System.Security.Cryptography.X509Certificates;
using _01_Framework.Application;
using _01_Framework.Infrastructure;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.EFCore.Repository
{
    public class ArticleRepository : RepositoryBase<Article,long>, IArticleReposiotry
    {
        private readonly BlogContext _context;
        
        public ArticleRepository(BlogContext context) :base(context)
        {
            _context = context;
        }
        

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            var query = _context.Articles.Include(x=> x.ArticleCategory).Select(x => new ArticleViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Category = x.ArticleCategory.Name,
                Picture = x.Picture,
                PublishDate = x.PublishDate.ToFarsi(),
                ShortDescription = x.ShortDescription.Substring(0, Math.Min(x.ShortDescription.Length,50)) + "..."
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Title))
                query = query.Where(x => x.Title.Contains(searchModel.Title));

            if (searchModel.CategoryId > 0)
                query = query.Where(x => x.CategoryId == searchModel.CategoryId);

            return query.OrderByDescending(x => x.Id).ToList();

        }

        public EditArticle GetDetails(long id)
        {
            return _context.Articles.Select(x => new EditArticle
            {
                Id = x.Id,
                Title = x.Title,
                ShortDescription = x.ShortDescription,
                Description = x.Description,
                PublishDate = x.PublishDate.ToFarsi(),
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
                MetaDescriptio = x.MetaDescriptio,
                Keywords = x.Keywords,
                CanonicalAddress = x.CanonicalAddress,
                CategoryId = x.CategoryId
            }).FirstOrDefault(x => x.Id == id);
        }

        public Article GetWithCategory(long id)
        {
            return _context.Articles
                .Include(x => x.ArticleCategory)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
