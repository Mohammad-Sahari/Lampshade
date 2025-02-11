namespace ShopManagement.Application.Contracts.Order
{
    public interface IOrderApplication
    {
        double GetAmountById(long id);
        long PlaceOrder(Cart cart);
        string PaymentSucceeded(long orderId, long refId);
        List<OrderViewModel> Search(OrderSearchModel searchModel);

        List<OrderItemViewModel> GetItemsBy(long orderId);
    }
}
