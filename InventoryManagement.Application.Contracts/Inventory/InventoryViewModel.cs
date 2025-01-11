namespace InventoryManagement.Application.Contracts.Inventory;

public class InventoryViewModel
{
    public long ProductId { get; private set; }
    public double UnitPrice { get; private set; }
    public string Product { get; set; }
    public long Id { get; set; }
    public bool InStock { get; set; }
    public long CurrentCount { get; set; }
}