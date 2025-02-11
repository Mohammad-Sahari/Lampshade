namespace InventoryManagement.Application.Contracts.Inventory;

public class DecreaseInventory
{
    public long Count { get; set; }
    public long ProductId { get; set; }
    public long InventoryId { get; set; }
    public string Description { get; set; }
    public long OrderId { get; set; }

    public DecreaseInventory()
    {
        
    }

    public DecreaseInventory(long count, long productId, string description, long orderId)
    {
        Count = count;
        ProductId = productId;
        Description = description;
        OrderId = orderId;
    }
}