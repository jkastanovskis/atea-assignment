using System;
using Billing.Order.Enums;
using Billing.Order.Exceptions;

namespace Billing.Order.Models
{
    public class OrderModel
    {
        public string OrderNumber { get; }
        public string UserId { get; }
        public double Amount { get; }
        public PaymentGateway PaymentGateway { get; }
        public string Description { get; }

        public OrderModel(string orderNumber, string userId, double amount, string paymentGateway, string description)
        {
            if (string.IsNullOrEmpty(orderNumber))
            {
                throw new BusinessException("orderNumber shouldn't be empty");
            }
            
            if (string.IsNullOrEmpty(userId))
            {
                throw new BusinessException("userId shouldn't be empty");
            }
            
            if (amount <= 0)
            {
                throw new BusinessException("amount should be greater than 0");
            }

            if (string.IsNullOrEmpty(paymentGateway))
            {
                throw new BusinessException("paymentGateway shouldn't be empty");
            }

            if (!Enum.TryParse<PaymentGateway>(paymentGateway, out var paymentGatewayEnum))
            {
                throw new BusinessException("Incorrect paymentGateway type");
            }
            
            OrderNumber = orderNumber;
            UserId = userId;
            Amount = amount;
            PaymentGateway = paymentGatewayEnum;
            Description = description;
        }
    }
}