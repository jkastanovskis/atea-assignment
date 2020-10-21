using System.Threading.Tasks;
using Billing.Order.Enums;
using Billing.Order.Models;
using Billing.Order.Services;
using NUnit.Framework;

namespace Billing.Tests.Order.Services
{
    public class BraintreePaymentProcessorTest
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public async Task BraintreePaymentProcessor_WillPass()
        {
            // arrange
            var order = new OrderModel(
                "1",
                "1",
                1,
                PaymentGateway.Braintree.ToString(),
                "1"
            );

            var braintreePaymentProcessor = new BraintreePaymentProcessor();
            // act
            var result = await braintreePaymentProcessor.ProcessAsync(order);
            // assert
            Assert.AreEqual("Braintree", result.Message);
        }
    }
}