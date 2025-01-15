using _01_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IPorductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IFileUploader _fileUploader;
        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository, IFileUploader fileUploader)
        {
            _productCategoryRepository = productCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            var operation = new OperationResult();
            if (_productCategoryRepository.Exists(x=> x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicateRecord);

            var slug = command.Slug.Slugify();
            var productCategory = new ProductCategory(command.Name, command.Description, "",
                command.PictureAlt, command.PictureTitle, command.Keywords,
                command.MetaDescription, slug);
            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operation = new OperationResult();
            var productCategory = _productCategoryRepository.Get(command.Id);
            //Null Check Guard
            if(productCategory is null)
                return operation.Failed(ApplicationMessages.NotFound);

            if (_productCategoryRepository.Exists(x=> x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicateRecord);

            var slug = command.Slug.Slugify();
            var picturePath = $"{command.Slug}";
            var fileName = _fileUploader.Upload(command.Picture,picturePath);
            productCategory.Edit(command.Name, command.Description, fileName,
                command.PictureAlt, command.PictureTitle, command.Keywords,
                command.MetaDescription, slug);
            _productCategoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
           return _productCategoryRepository.Search(searchModel);
        }

        public List<ProductCategoryViewModel> GetProductCategories()
        {
            return _productCategoryRepository.GetProductCategories();
        }

        public EditProductCategory GetDetails(long id)
        {
            return _productCategoryRepository.GetDetails(id);
        }
    }
}
