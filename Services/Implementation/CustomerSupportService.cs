using ecommerce_dotnet.Data;
using ecommerce_dotnet.Models;
using ecommerce_dotnet.Services.Exceptions;
using ecommerce_dotnet.Services.Interfaces;
using ecommerce_dotnet.Utility;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_dotnet.Services.Implementation
{
    public class CustomerSupportService : ICustomerSupportService
    {
        private readonly AppDbContext _dbContext;

        public CustomerSupportService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CustomerSupport?> FindAsync(string id)
        {
            return await _dbContext.CustomerSupports
                .Include(_ => _.Messages).FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task AddMessageAsync(CustomerSupport ticket, Message message)
        {
            if (ticket.IsClosed)
                throw new TicketClosedException(Constants.Exception.TicketClosed);

            ticket.Messages.Add(message);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddMessageAsync(string id, Message message)
        {
            CustomerSupport? ticket = await FindAsync(id);
            if (ticket == null)
                throw new TicketNotFoundException(Constants.Exception.TicketNotFound);

            await AddMessageAsync(ticket, message);
        }

        public async Task CloseTicket(CustomerSupport ticket)
        {
            ticket.IsClosed = true;

            _dbContext.CustomerSupports.Update(ticket);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CloseTicket(string id)
        {
            CustomerSupport? ticket = await FindAsync(id);
            if (ticket == null)
                throw new TicketNotFoundException(Constants.Exception.TicketNotFound);

            await CloseTicket(ticket);
        }
    }
}
