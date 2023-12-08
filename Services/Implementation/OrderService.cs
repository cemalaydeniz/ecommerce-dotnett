using ecommerce_dotnet.Data;
using ecommerce_dotnet.Models;
using ecommerce_dotnet.Services.Interfaces;
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
            return await _dbContext.Orders.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<List<Order>> FindAllAsync(Expression<Func<Order, bool>> query)
        {
            return await _dbContext.Orders.Where(query).ToListAsync();
        }

        public async Task BulkAddAsync(List<Order> orders)
        {
            _dbContext.AddRange(orders);
            await _dbContext.SaveChangesAsync();
        }
    }
}
