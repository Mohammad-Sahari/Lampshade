using _01_Framework.Application;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
    public class ArticleApplication : IArticleApplication
    {
        private readonly IArticleReposiotry _articleReposiotry;
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IFileUploader _fileUploader;
        public ArticleApplication(IArticleReposiotry articleReposiotry, IFileUploader fileUploader, IArticleCategoryRepository articleCategoryRepository)
        {
            _articleReposiotry = articleReposiotry;
            _fileUploader = fileUploader;
            _articleCategoryRepository = articleCategoryRepository;
        }

        public OperationResult Create(CreateArticle command)
        {
            var operation = new OperationResult();
            if (_articleReposiotry.Exists(x => x.Title == command.Title))
                return operation.Failed(ApplicationMessages.DuplicateRecord);

            var publishDate = command.PublishDate.ToGeorgianDateTime();
            var slug = command.Slug.Slugify();
            var categorySlug = _articleCategoryRepository.GetSlugBy(command.CategoryId);
            var path = $"{categorySlug}/{slug}";
            var pictureName = _fileUploader.Upload(command.Picture, path);
            var article = new Article(command.Title, command.ShortDescription, command.Description, pictureName,
                command.PictureAlt, command.PictureTitle, publishDate, slug, command.MetaDescriptio, command.Keywords,
                command.CanonicalAddress, command.CategoryId);

            _articleReposiotry.Create(article);
            _articleReposiotry.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditArticle command)
        {
            var operation = new OperationResult();
            var article = _articleReposiotry.GetWithCategory(command.Id);

            if (article is null)
                return operation.Failed(ApplicationMessages.NotFound);

            if (_articleReposiotry.Exists(x => x.Title == command.Title && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.NotFound);

            var publishDate = command.PublishDate.ToGeorgianDateTime();
            var slug = command.Slug.Slugify();
            var path = $"{article.ArticleCategory.Slug}/{slug}";
            var pictureName = _fileUploader.Upload(command.Picture, path);
            article.Edit(command.Title, command.ShortDescription, command.Description, pictureName,
                command.PictureAlt, command.PictureTitle, publishDate, slug, command.MetaDescriptio, command.Keywords,
                command.CanonicalAddress, command.CategoryId);
            _articleReposiotry.SaveChanges();
            return operation.Succeeded();
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            return _articleReposiotry.Search(searchModel);
        }

        public EditArticle GetDetails(long id)
        {
            return _articleReposiotry.GetDetails(id);
        }
    }
}
