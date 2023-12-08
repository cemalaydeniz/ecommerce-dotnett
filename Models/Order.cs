using System.ComponentModel.DataAnnotations;

namespace ecommerce_dotnet.Models
{
    public class Order
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        // Foreign keys
        [Required]
        public string UserId { get; set; } = null!;
        public virtual User User { get; set; } = null!;

        [Required]
        public string ProductId { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;

        public string? CustomerSupportId { get; set; } = null!;
        public virtual CustomerSupport? CustomerSupport { get; set; } = null!;
    }
}
