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
    }
}
