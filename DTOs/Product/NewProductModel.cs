namespace ecommerce_dotnet.DTOs.Product
{
    public class NewProductModel
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }
    }
}
