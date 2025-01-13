using _01_Framework.Domain;
using InventoryManagement.Application.Contracts.Inventory;

namespace InventoryManagement.Domain.InventoryAgg;

public interface IInventoryRepository : IRepository<Inventory, long>
{
    EditInventory GetDetails(long id);
    Inventory GetBy(long productId);
    List<InventoryViewModel> Search(InventorySearchModel searchModel);
    List<InventoryOperationViewModel> GetOperationLog(long inventoryId);
}