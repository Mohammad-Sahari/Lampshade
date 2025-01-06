using _01_Framework.Domain;
using ShopManagement.Application.Contracts.ProductPicture;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository : IRepository<ProductPicture, long>
    {
        EditProductPicture GetDetails(long id);
        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
    }
}
