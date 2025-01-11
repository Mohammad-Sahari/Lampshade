namespace InventoryManagement.Application.Contracts.Inventory;

public class CreateInventory
{
    public long ProductId { get; private set; }
    public double UnitPrice { get; private set; }
}