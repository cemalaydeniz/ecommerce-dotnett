using ecommerce_dotnet.Data;
using ecommerce_dotnet.Models;
using ecommerce_dotnet.Services.Exceptions;
using ecommerce_dotnet.Services.Interfaces;
using ecommerce_dotnet.Utility;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ecommerce_dotnet.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _dbContext;

        public OrderService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Order order)
        {
            _dbContext.Add(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Order?> FindAsync(string id)
        {
            return await _dbContext.Orders
                .Include(_ => _.User)
                .Include(_ => _.Product)
                .Include(_ => _.CustomerSupport).FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<List<Order>> FindAllAsync(Expression<Func<Order, bool>> query)
        {
            return await _dbContext.Orders
                .Include(_ => _.User)
                .Include(_ => _.Product)
                .Include(_ => _.CustomerSupport).Where(query).ToListAsync();
        }

        public async Task BulkAddAsync(List<Order> orders)
        {
            _dbContext.AddRange(orders);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddTicketAsync(Order order, CustomerSupport ticket)
        {
            order.CustomerSupport = ticket;
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddTicketAsync(string orderId, CustomerSupport ticket)
        {
            Order? order = await FindAsync(orderId);
            if (order == null)
                throw new OrderNotFoundException(Constants.Exception.OrderNotFound);

            await AddTicketAsync(order, ticket);
        }
    }
}
