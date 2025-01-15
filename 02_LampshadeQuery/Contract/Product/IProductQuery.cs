namespace _02_LampshadeQuery.Contract.Product
{
    public interface IProductQuery
    {
        List<ProductQueryModel> GetLatestProducts();
        List<ProductQueryModel> Search(string value);
    }
}
