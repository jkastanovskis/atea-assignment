using System.Collections.Generic;
using System.Threading.Tasks;
using Billing.Order.Enums;
using Billing.Order.Exceptions;
using Billing.Order.Models;
using Billing.Order.Services;
using Billing.Order.Services.interfaces;
using Moq;
using NUnit.Framework;

namespace Billing.Tests.Order.Services
{
    public class BillingServiceTest
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public Task BillingService_OrderWithUnimplementedPaymentProcessor_WillThrowException()
        {
            // arrange
            var order = new OrderModel(
                "1",
                "1",
                1,
                PaymentGateway.Braintree.ToString(),
                "1"
            );

            var paymentProcesorMock = new Mock<IPaymentProcessor>();
            var billingService = new BillingService(new List<IPaymentProcessor>() {paymentProcesorMock.Object});
            paymentProcesorMock.SetupGet(x => x.PaymentGateway).Returns(PaymentGateway.SagePay);
            // act
            var e = Assert.ThrowsAsync<BusinessException>(async () => await billingService.ProcessAsync(order));
            // assert
            Assert.AreEqual(
                $"Payment processor for payment gateway '{PaymentGateway.Braintree.ToString()}' is not implemented",
                e.Message);

            return Task.CompletedTask;
        }

        [Test]
        public async Task BillingService_WillPass()
        {
            // arrange
            var order = new OrderModel(
                "1",
                "1",
                1,
                PaymentGateway.Braintree.ToString(),
                "1"
            );

            var receipt = new ReceiptModel("Test");

            var paymentProcesorMock = new Mock<IPaymentProcessor>();
            var billingService = new BillingService(new List<IPaymentProcessor>() {paymentProcesorMock.Object});
            paymentProcesorMock.SetupGet(x => x.PaymentGateway).Returns(PaymentGateway.Braintree);
            paymentProcesorMock.Setup(x => x.ProcessAsync(It.IsAny<OrderModel>())).ReturnsAsync(receipt);
            // act
            var resultReceipt = await billingService.ProcessAsync(order);
            // assert
            Assert.AreEqual(receipt, resultReceipt);
        }
    }
}