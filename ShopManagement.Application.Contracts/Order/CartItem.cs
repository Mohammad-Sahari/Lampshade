namespace ShopManagement.Application.Contracts.Order
{
    public class CartItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Picture { get; set; }
        public int Count { get; set; }
        public double TotalItemPrice => Price * Count;
        public bool IsInStock { get; set; }
        //OPTIMIZE: create a guard to make sure DR is between 0 and 100.(logic applied in CD domain)
        public int DiscountRate { get; set; }
        public double DiscountAmount => (TotalItemPrice * DiscountRate / 100);
        //OPTIMIZE: Create a guard to make sure DS will not be higher than TPA.
        public double ItemPayAmount => TotalItemPrice - DiscountAmount;
    }
}
