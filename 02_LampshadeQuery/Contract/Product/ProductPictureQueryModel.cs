namespace _02_LampshadeQuery.Contract.Product;

public class ProductPictureQueryModel
{
    public long Id { get; set; }
    public string Picture { get; set; }
    public string PictureAlt { get; set; }
    public string PictureTitle { get; set; }
    public long ProductId { get; set; }
    public bool IsRemoved { get; set; }
}