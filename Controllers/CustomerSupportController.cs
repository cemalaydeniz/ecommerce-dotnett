using ecommerce_dotnet.DTOs.CustomerSupport;
using ecommerce_dotnet.Models;
using ecommerce_dotnet.Services.Interfaces;
using ecommerce_dotnet.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace ecommerce_dotnet.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerSupportController : Controller
    {
        private readonly IOrderService _orderService;

        public CustomerSupportController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("new/{orderId}")]
        public async Task<IActionResult> NewTicket(string orderId, [FromBody]TicketModel ticketModel)
        {
            if (ticketModel == null)
                return BadRequest(JsonResponse.Error(Constants.Response.General.BadRequest));

            string? email = User.FindFirst(ClaimTypes.Name)?.Value;
            if (email == null)
                return StatusCode((int)HttpStatusCode.Unauthorized, JsonResponse.Error(Constants.Response.General.Unauthorized));

            await _orderService.AddTicketAsync(orderId, new CustomerSupport()
            {
                OrderId = orderId,
                Messages = new List<Message>() { new Message()
                {
                    Email = email,
                    Content = ticketModel.Message
                } }
            });

            return StatusCode((int)HttpStatusCode.Created, JsonResponse.Success(Constants.Response.CustomerSupport.TicketCreated));
        }
    }
}
