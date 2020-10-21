using System.Threading.Tasks;
using Billing.Order.Enums;
using Billing.Order.Models;
using Billing.Order.Services.interfaces;
using Microsoft.Extensions.DependencyInjection;
using ServiceLocator;

namespace Billing.Order.Services
{
    [Service(ServiceLifetime.Scoped)]
    public class BraintreePaymentProcessor: IPaymentProcessor
    {
        public PaymentGateway PaymentGateway => PaymentGateway.Braintree;
        
        public Task<ReceiptModel> ProcessAsync(OrderModel order)
        {
            return Task.FromResult(new ReceiptModel("Braintree"));
        }
    }
}