using ecommerce_dotnet.DTOs.Order;
using ecommerce_dotnet.Models;
using ecommerce_dotnet.Services.Interfaces;
using ecommerce_dotnet.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace ecommerce_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IConfiguration _configuration;

        public OrderController(UserManager<User> userManager, IProductService productService, IOrderService orderService, IConfiguration configuration)
        {
            _userManager = userManager;
            _productService = productService;
            _orderService = orderService;
            _configuration = configuration;
        }

        [Authorize]
        [HttpPost("payment")]
        public async Task<IActionResult> Pay([FromBody]CartModel cartModel)
        {
            if (cartModel == null || cartModel.Items.Count == 0)
                return BadRequest(JsonResponse.Error(Constants.Response.General.BadRequest));

            User? user = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.Name)?.Value);
            if (user == null)
                return StatusCode((int)HttpStatusCode.Unauthorized, JsonResponse.Error(Constants.Response.General.Unauthorized));

            // In order to check if the all the products in the cart exist on the database. If not, do not continue on the payment process
            List<Product> products = await _productService.FindAllAsync(p => cartModel.Items.Any(i => i.ProductId == p.Id));
            if (products == null || products.Count != cartModel.Items.Count)
                return BadRequest(JsonResponse.Error(Constants.Response.General.BadRequest));

            decimal totalAmount = 0;
            products.ForEach(_ => totalAmount += _.Price);

            var paymentIntent = new Stripe.PaymentIntentService().Create(new Stripe.PaymentIntentCreateOptions()
            {
                Amount = (long)(totalAmount * 100),
                Currency = "usd",
                Customer = user.Id,
                Metadata = cartModel.Items.ToDictionary(_ => _.ProductId, _ => _.Quantity.ToString()),
                ReceiptEmail = user.Email
            });

            return Ok(JsonResponse.Data(true, paymentIntent.ClientSecret));
        }

        [HttpPost("payment/webhook")]
        public async Task<IActionResult> PaymentCheck()
        {
            string endpointSecret = _configuration["StripeSecret"];

            var requestBody = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var signatureHeader = Request.Headers["Stripe-Signature"];
            var stripeEvent = Stripe.EventUtility.ConstructEvent(requestBody, signatureHeader, endpointSecret);

            if (stripeEvent.Type == Stripe.Events.PaymentIntentSucceeded)
            {
                var paymentIntent = (Stripe.PaymentIntent)stripeEvent.Data.Object;

                List<Order> orders = new List<Order>();
                foreach (var item in paymentIntent.Metadata)
                {
                    orders.Add(new Order()
                    {
                        UserId = paymentIntent.CustomerId,
                        ProductId = item.Key,
                        Quantity = int.Parse(item.Value)
                    });
                }

                await _orderService.BulkAddAsync(orders);
            }

            return Ok();
        }
    }
}
