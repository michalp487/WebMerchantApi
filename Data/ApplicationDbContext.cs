using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebMerchantApi.Entities;
using WebMerchantApi.Models;

namespace WebMerchantApi.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<BasketItem> BasketItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(10, 3)");

            builder.Entity<BasketItem>()
                .HasOne(x => x.User)
                .WithMany(x => x.BasketItems)
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            builder.Entity<BasketItem>()
                .Property(p => p.Price)
                .HasColumnType("decimal(10, 3)");

            builder.Entity<Order>()
                .HasOne(x => x.User)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            builder.Entity<Order>()
                .Property(p => p.Price)
                .HasColumnType("decimal(10, 3)");
        }
    }
}
