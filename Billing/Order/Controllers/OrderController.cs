using System;
using System.Threading.Tasks;
using Billing.Order.Enums;
using Billing.Order.Exceptions;
using Billing.Order.Models;
using Billing.Order.Services.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Logging;

namespace Billing.Order.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IBillingService _billingService;

        public OrderController(ILogger<OrderController> logger, IBillingService billingService)
        {
            _logger = logger;
            _billingService = billingService;
        }

        [HttpPost]
        [Route("Process")]
        public async Task<ActionResult<ReceiptModel>> Process([FromBody] OrderProcessRequest orderProcess)
        {
            try
            {
                var order = new OrderModel(orderProcess.OrderNumber, orderProcess.UserId, orderProcess.Amount,
                    orderProcess.PaymentGateway, orderProcess.Description);

                var receipt = await _billingService.ProcessAsync(order);

                return Ok(receipt);
            }
            catch (BusinessException e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest("Failed processing order");
            }
        }
    }
}