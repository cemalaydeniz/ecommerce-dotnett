using ecommerce_dotnet.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_dotnet.Data
{
    public class AppDbContext : IdentityDbContext<User, Role, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().HasIndex(_ => _.Name);       // If the users are searched by their names often
            builder.Entity<User>()
                .Ignore(_ => _.AccessFailedCount)
                .Ignore(_ => _.EmailConfirmed)
                .Ignore(_ => _.LockoutEnabled)
                .Ignore(_ => _.LockoutEnd)
                .Ignore(_ => _.PhoneNumberConfirmed)
                .Ignore(_ => _.TwoFactorEnabled);
            // Foreign keys of User
            builder.Entity<User>()
                .HasMany(_ => _.Orders)
                .WithOne(_ => _.User)
                .HasForeignKey(_ => _.UserId);

            builder.Entity<Product>().HasIndex(_ => _.Name);    // If the products are searched by their names often

            // Foreign keys of Order
            builder.Entity<Order>()
                .HasOne(_ => _.CustomerSupport)
                .WithOne(_ => _.Order)
                .HasForeignKey<CustomerSupport>(_ => _.OrderId);

            // Foreign keys of CustomerSupport
            builder.Entity<CustomerSupport>()
                .HasOne(_ => _.Order)
                .WithOne(_ => _.CustomerSupport)
                .HasForeignKey<Order>(_ => _.CustomerSupportId);
            builder.Entity<CustomerSupport>()
                .HasMany(_ => _.Messages)
                .WithOne(_ => _.CustomerSupport)
                .HasForeignKey(_ => _.CustomerSupportId);
        }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<CustomerSupport> CustomerSupports { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;
    }
}
