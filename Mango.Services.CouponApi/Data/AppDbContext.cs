using Mango.Services.CouponApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(
            new Coupon{ Id = 1 , CouponCode = "10OFF", DiscountAmount = 10,MinAmount = 20},
            new Coupon { Id = 2, CouponCode = "20OFF", DiscountAmount = 20, MinAmount = 40 });
        }
    }
}
