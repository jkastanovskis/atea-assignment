namespace Billing.Order.Models
{
    public class OrderProcessRequest
    {
        public string OrderNumber { get; set; }
        public double Amount { get; set; }
        public string UserId { get; set; }
        public string PaymentGateway { get; set; }
        public string Description { get; set; }
    }
}