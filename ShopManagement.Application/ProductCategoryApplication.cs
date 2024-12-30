using _01_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IPorductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            var operation = new OperationResult();
            if (_productCategoryRepository.Exists(x=> x.Name == command.Name))
                return operation.Failed("این رکورد تکراری می باشد");

            var slug = command.Slug.Slugify();
            var productCategory = new ProductCategory(command.Name, command.Description, command.Picture,
                command.PictureAlt, command.PictureTitle, command.Keywords,
                command.MetaDescription, slug);
            _productCategoryRepository.Add(productCategory);
            _productCategoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operation = new OperationResult();
            var productCategory = _productCategoryRepository.Get(command.Id);
            if(productCategory is null)
                return operation.Failed("رکورد یافت نشد");

            if (_productCategoryRepository.Exists(x=> x.Name == command.Name && x.Id != command.Id))
                return operation.Failed("این رکورد تکراری می باشد");

            var slug = command.Slug.Slugify();
            productCategory.Edit(command.Name, command.Description, command.Picture,
                command.PictureAlt, command.PictureTitle, command.Keywords,
                command.MetaDescription, slug);
            _productCategoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
           return _productCategoryRepository.Search(searchModel);
        }

        public EditProductCategory GetDetails(long id)
        {
            return _productCategoryRepository.GetDetails(id);
        }
    }
}
