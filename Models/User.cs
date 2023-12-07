using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ecommerce_dotnet.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(15)]
        public override string? PhoneNumber { get; set; }

        [MaxLength(255)]
        public string? Address { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign keys
        public virtual ICollection<Order> Orders { get; set; } = null!;
    }
}
