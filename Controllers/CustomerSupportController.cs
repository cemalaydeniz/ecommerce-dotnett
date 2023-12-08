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
        private readonly ICustomerSupportService _customerSupportService;

        public CustomerSupportController(IOrderService orderService, ICustomerSupportService customerSupportService)
        {
            _orderService = orderService;
            _customerSupportService = customerSupportService;
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

        [HttpPost("reply/{ticketId}")]
        public async Task<IActionResult> NewMessage(string ticketId, [FromBody]TicketModel ticketModel)
        {
            string? email = User.FindFirst(ClaimTypes.Name)?.Value;
            if (email == null)
                return BadRequest(JsonResponse.Error(Constants.Response.General.Unauthorized));

            CustomerSupport? ticket = await _customerSupportService.FindAsync(ticketId);
            if (ticket == null)
                return NotFound(JsonResponse.Error(Constants.Response.CustomerSupport.TicketNotFound));

            // Only the owner of the ticket or the admins can reply to the ticket
            if (ticket.Messages.ElementAt(0).Email != email && !User.IsInRole(Constants.Roles.Admin))
                return BadRequest(JsonResponse.Error(Constants.Response.General.Unauthorized));

            await _customerSupportService.AddMessageAsync(ticket, new Message()
            {
                Email = email,
                Content = ticketModel.Message
            });

            return Ok(JsonResponse.Success(Constants.Response.CustomerSupport.MessageSent));
        }

        [Authorize(Roles = Constants.Roles.Admin)]
        [HttpDelete("close/{ticketId}")]
        public async Task<IActionResult> CloseTicket(string ticketId)
        {
            await _customerSupportService.CloseTicket(ticketId);

            return Ok(JsonResponse.Success(Constants.Response.CustomerSupport.TicketClosed));
        }
    }
}
