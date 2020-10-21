using System.Threading.Tasks;
using Billing.Order.Models;

namespace Billing.Order.Services.interfaces
{
    public interface IBillingService
    {
        Task<ReceiptModel> ProcessAsync(OrderModel order);
    }
}