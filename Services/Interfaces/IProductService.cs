using ecommerce_dotnet.Models;
using System.Linq.Expressions;

namespace ecommerce_dotnet.Services.Interfaces
{
    public interface IProductService
    {
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task<Product?> FindAsync(string id);
        Task<List<Product>> FindAllAsync(Expression<Func<Product, bool>> query);
        Task<List<Product>> GetAllAsync();
        Task RemoveAsync(string id);
        Task RemoveAsync(Product product);

        Task BulkAddAsync(List<Product> products);
        Task BulkEditAsync(List<Product> products);
        Task BulkRemoveAsync(List<string> ids);
        Task BulkRemoveAsync(List<Product> products);

        Task<List<Product>> SearchByNameAsync(string name, int page, int pageSize, bool includeDeleted = false);
    }
}
