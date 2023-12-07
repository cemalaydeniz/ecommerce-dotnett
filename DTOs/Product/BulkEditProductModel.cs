namespace ecommerce_dotnet.DTOs.Product
{
    public class BulkEditProductModel
    {
        public class UpdateProduct
        {
            public string Id { get; set; } = null!;
            public string? Name { get; set; }
            public decimal? Price { get; set; }
            public string? Description { get; set; }
        }

        public List<UpdateProduct> Products { get; set; } = null!;
    }
}
