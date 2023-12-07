using System.ComponentModel.DataAnnotations;

namespace ecommerce_dotnet.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        [Range(10, 100)]
        public string Content { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign keys
        [Required]
        public string CustomerSupportId { get; set; } = null!;
        public virtual CustomerSupport CustomerSupport { get; set; } = null!;
    }
}
