using ecommerce_dotnet.Models;
using System.Linq.Expressions;

namespace ecommerce_dotnet.Services.Interfaces
{
    public interface IOrderService
    {
        Task AddAsync(Order order);
        Task<Order?> FindAsync(string id);
        Task<List<Order>> FindAllAsync(Expression<Func<Order, bool>> query);

        Task BulkAddAsync(List<Order> orders);

        Task AddTicketAsync(Order order, CustomerSupport ticket);
        Task AddTicketAsync(string orderId, CustomerSupport ticket);
    }
}
