using Billing.Order.Enums;
using Billing.Order.Exceptions;
using Billing.Order.Models;
using NUnit.Framework;

namespace Billing.Tests.Order.Models
{
    public class OrderModelTest
    {
        private void SetUp()
        {
        }

        [Test]
        public void Order_MissingOrderNumber_WillThrowException()
        {
            // arrange
            var orderNumber = "";
            // act
            // assert
            var e = Assert.Throws<BusinessException>(() => new OrderModel(
                orderNumber,
                "1",
                1,
                PaymentGateway.Braintree.ToString(),
                null
            ));
            Assert.AreEqual("orderNumber shouldn't be empty", e.Message);
        }
        
        [Test]
        public void Order_MissingUserId_WillThrowException()
        {
            // arrange
            var userId = "";
            // act
            // assert
            var e = Assert.Throws<BusinessException>(() => new OrderModel(
                "1",
                userId,
                1,
                PaymentGateway.Braintree.ToString(),
                null
            ));
            Assert.AreEqual("userId shouldn't be empty", e.Message);
        }
        
        [Test]
        public void Order_AmountLessThanZero_WillThrowException()
        {
            // arrange
            var amount = -1;
            // act
            // assert
            var e = Assert.Throws<BusinessException>(() => new OrderModel(
                "1",
                "1",
                amount,
                PaymentGateway.Braintree.ToString(),
                null
            ));
            Assert.AreEqual("amount should be greater than 0", e.Message);
        }
        
        [Test]
        public void Order_MissingPaymentGateway_WillThrowException()
        {
            // arrange
            var paymentGateway = "";
            // act
            // assert
            var e = Assert.Throws<BusinessException>(() => new OrderModel(
                "1",
                "1",
                1,
                paymentGateway,
                null
            ));
            Assert.AreEqual("paymentGateway shouldn't be empty", e.Message);
        }
        
        [Test]
        public void Order_InvalidPaymentGateway_WillThrowException()
        {
            // arrange
            var paymentGateway = "Test";
            // act
            // assert
            var e = Assert.Throws<BusinessException>(() => new OrderModel(
                "1",
                "1",
                1,
                paymentGateway,
                null
            ));
            Assert.AreEqual("Incorrect paymentGateway type", e.Message);
        }
        
        [Test]
        public void Order_CorrectData_WillPass()
        {
            // arrange
            var orderNumber = "1";
            var userId = "1";
            var amount = 1;
            var description = "1";
            var paymentGateway = PaymentGateway.Braintree.ToString();
            var paymentGatewayEnum = PaymentGateway.Braintree;
            // act
            var order = new OrderModel(
                orderNumber,
                userId,
                amount,
                paymentGateway,
                description
            );
            // assert
            Assert.AreEqual(orderNumber, order.OrderNumber);
            Assert.AreEqual(userId, order.UserId);
            Assert.AreEqual(amount, order.Amount);
            Assert.AreEqual(description, order.Description);
            Assert.AreEqual(paymentGatewayEnum, order.PaymentGateway);
        }
    }
}