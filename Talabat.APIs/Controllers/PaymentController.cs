using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Services.Contract;

namespace Talabat.APIs.Controllers
{
    public class PaymentController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;


        // This is your Stripe CLI webhook secret for testing your endpoint locally.
        private const string whSecret = "whsec_21e956b85fafdf24d5876c7ef266cdabbba97e0ab0adf9838ab2c0d64d39da95";

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger )
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [Authorize]
        [ProducesResponseType(typeof(CustomerBasket), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpGet("{basketId}")] // GET : /api/Payment/{basketid}
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            if (basket is null) return BadRequest(new ApiResponse(400, "An Error with your Basket"));

            return Ok(basket);
        }


        [HttpPost("webhook")]
        public async Task<IActionResult> webHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json,
                Request.Headers["Stripe-Signature"], whSecret);


            var paymentIntent = (PaymentIntent)stripeEvent.Data.Object;

            Order? order;
            // Handle the event

            switch (stripeEvent.Type)
            {
                case Events.PaymentIntentSucceeded:
                    order = await _paymentService.UpdateOrderStatus(paymentIntent.Id, true);

                    _logger.LogInformation("Order is Succeeded {0}", order?.PaymentIntentId);
                    _logger.LogInformation("Unhandeled event type: {0}", stripeEvent.Type);

                    break;
                case Events.PaymentIntentPaymentFailed:
                    order = await _paymentService.UpdateOrderStatus(paymentIntent.Id, false);

                    _logger.LogInformation("Order is Failed! {0}", order?.PaymentIntentId);
                    _logger.LogInformation("Unhandeled event type: {0}", stripeEvent.Type);


                    break;

            }


            return Ok();



        }
    }
}
