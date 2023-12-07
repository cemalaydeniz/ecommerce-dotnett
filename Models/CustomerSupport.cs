using System.ComponentModel.DataAnnotations;

namespace ecommerce_dotnet.Models
{
    public class CustomerSupport
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public bool IsClosed { get; set; } = false;

        // Foreign keys
        [Required]
        public string OrderId { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;

        [Required]
        public virtual ICollection<Message> Messages { get; set; } = null!;
    }
}
