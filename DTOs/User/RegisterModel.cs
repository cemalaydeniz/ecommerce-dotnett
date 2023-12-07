namespace ecommerce_dotnet.DTOs.User
{
    public class RegisterModel
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? PhoneNumber { get; set; } = null!;
        public string? Address { get; set; } = null!;
    }
}
