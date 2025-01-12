using _01_Framework.Application;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;

namespace InventoryManagement.Application
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IInventoryRepository _inventoryReposiotry;

        public InventoryApplication(IInventoryRepository inventoryReposiotry)
        {
            _inventoryReposiotry = inventoryReposiotry;
        }

        public OperationResult Create(CreateInventory command)
        {
            var operation = new OperationResult();
            if (_inventoryReposiotry.Exists(x => x.ProductId == command.ProductId))
                return operation.Failed(ApplicationMessages.DuplicateRecord);
            var inventory = new Inventory(command.ProductId, command.UnitPrice);
            _inventoryReposiotry.Create(inventory);
            _inventoryReposiotry.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryReposiotry.Get(command.Id);
            if (inventory is null)
                return operation.Failed(ApplicationMessages.NotFound);

            if (_inventoryReposiotry.Exists(x => x.ProductId == command.ProductId && x.Id == command.Id))
                return operation.Failed(ApplicationMessages.DuplicateRecord);

            inventory.Edit(command.ProductId, command.UnitPrice);
            _inventoryReposiotry.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Increase(IncreaseInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryReposiotry.Get(command.InventoryId);
            if (inventory is null)
                return operation.Failed(ApplicationMessages.NotFound);

            const long operatorId = 1;
            inventory.Increase(command.Count, operatorId, command.Description);
            _inventoryReposiotry.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Decrease(List<DecreaseInventory> command)
        {
            var operation = new OperationResult();
            const long operatorId = 1;
            foreach (var item in command)
            {
                var inventory = _inventoryReposiotry.GetBy(item.ProductId);
                inventory.Reduce(item.Count,operatorId,item.Description,item.OrderId);

            }
            _inventoryReposiotry.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Decrease(DecreaseInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryReposiotry.Get(command.InventoryId);
            if(inventory is null)
            return operation.Failed(ApplicationMessages.NotFound);

            const long operatorId = 1;
            inventory.Reduce(command.Count, operatorId, command.Description, 0);
            _inventoryReposiotry.SaveChanges();
            return operation.Succeeded();
        }

        public EditInventory GetDetails(long id)
        {
            return _inventoryReposiotry.GetDetails(id);
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            return _inventoryReposiotry.Search(searchModel);
        }
    }
}
