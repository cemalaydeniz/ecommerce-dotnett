using AutoMapper;
using ecommerce_dotnet.DTOs.Product;
using ecommerce_dotnet.Models;

namespace ecommerce_dotnet.Mappings
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<NewProductModel, Product>();
        }
    }
}
