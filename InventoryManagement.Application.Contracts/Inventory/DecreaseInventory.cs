﻿namespace InventoryManagement.Application.Contracts.Inventory;

public class DecreaseInventory
{
    public long Count { get; set; }
    public long ProductId { get; set; }
    public string Description { get; set; }
    public long OrderId { get; set; }

}