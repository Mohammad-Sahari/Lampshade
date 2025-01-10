using _01_Framework.Domain;

namespace DiscountManagement.domain.CustomerDiscountAgg
{
    public class CustomerDiscount : EntityBase
    {
        public long ProductId { get; private set; }
        public int DiscountRate { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string EventName { get; private set; }

        public CustomerDiscount(long productId, int discountRate, DateTime startDate, DateTime endDate, string eventName)
        {
            ProductId = productId;
            DiscountRate = discountRate;
            StartDate = startDate;
            EndDate = endDate;
            EventName = eventName;
        }

        public void Edit(long productId, int discountRate, DateTime startDate, DateTime endDate, string eventName)
        {
            ProductId = productId;
            DiscountRate = discountRate;
            StartDate = startDate;
            EndDate = endDate;
            EventName = eventName;
        }
    }
}
