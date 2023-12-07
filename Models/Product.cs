using System.ComponentModel.DataAnnotations;

namespace ecommerce_dotnet.Models
{
    public class Product
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }

        public string? Description { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
