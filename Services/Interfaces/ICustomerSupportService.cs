using ecommerce_dotnet.Models;

namespace ecommerce_dotnet.Services.Interfaces
{
    public interface ICustomerSupportService
    {
        Task<CustomerSupport?> FindAsync(string id);

        Task AddMessageAsync(CustomerSupport ticket, Message message);
        Task AddMessageAsync(string id, Message message);
        Task CloseTicket(CustomerSupport ticket);
        Task CloseTicket(string id);
    }
}
