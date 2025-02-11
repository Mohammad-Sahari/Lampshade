using InventoryManagement.Application.Contracts.Inventory;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;

namespace ShopManagement.Infrastructure.InventoryAcl
{
    public class ShopInventoryAcl : IShopInventoryAcl
    {
        private readonly IInventoryApplication _inventoryApplication;

        public ShopInventoryAcl(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
        }

        public bool ReduceFromInventory(List<OrderItem> items)
        {
            var command = new List<DecreaseInventory>();
            foreach (var orderItem in items)
            {
                var item = new DecreaseInventory(orderItem.Count,orderItem.ProductId,"خرید مشتری",orderItem.OrderId);
                command.Add(item);
            }

            return _inventoryApplication.Decrease(command).IsSucceeded;
        }
    } 
}
