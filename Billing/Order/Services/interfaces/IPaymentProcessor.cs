using System.Threading.Tasks;
using Billing.Order.Enums;
using Billing.Order.Models;

namespace Billing.Order.Services.interfaces
{
    public interface IPaymentProcessor
    {
        PaymentGateway PaymentGateway { get; }
        Task<ReceiptModel> ProcessAsync(OrderModel order);
    }
}