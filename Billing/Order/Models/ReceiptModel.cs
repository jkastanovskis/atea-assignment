namespace Billing.Order.Models
{
    public class ReceiptModel
    {
        public string Message { get; }

        public ReceiptModel(string message)
        {
            Message = message;
        }
    }
}