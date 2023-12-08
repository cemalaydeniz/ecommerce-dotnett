namespace ecommerce_dotnet.DTOs.Order
{
    public class CartModel
    {
        public class Item
        {
            public string ProductId { get; set; } = null!;
            public int Quantity { get; set; }
        }

        public List<Item> Items { get; set; } = null!;
    }
}
