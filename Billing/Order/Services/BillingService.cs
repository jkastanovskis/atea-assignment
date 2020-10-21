using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Billing.Order.Exceptions;
using Billing.Order.Models;
using Billing.Order.Services.interfaces;
using Microsoft.Extensions.DependencyInjection;
using ServiceLocator;

namespace Billing.Order.Services
{
    [Service(ServiceLifetime.Scoped)]
    public class BillingService: IBillingService
    {
        private readonly IEnumerable<IPaymentProcessor> _paymentProcessors;

        public BillingService(IEnumerable<IPaymentProcessor> paymentProcessors)
        {
            _paymentProcessors = paymentProcessors;
        }
        public async Task<ReceiptModel> ProcessAsync(OrderModel order)
        {
            var paymentProcessor = _paymentProcessors.FirstOrDefault(x => x.PaymentGateway == order.PaymentGateway);

            if (paymentProcessor == null)
            {
                throw new BusinessException($"Payment processor for payment gateway '{order.PaymentGateway}' is not implemented");
            }

            var receipt = await paymentProcessor.ProcessAsync(order);

            return receipt;
        }
    }
}