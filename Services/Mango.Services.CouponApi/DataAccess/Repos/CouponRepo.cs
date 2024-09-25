using Mango.Services.CouponApi.Data;
using Mango.Services.CouponApi.DataAccess.IRepos;
using Mango.Services.CouponApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponApi.DataAccess.Repos
{
    public class CouponRepo : GenericRepo<Coupon>, ICouponRepo
    {
        private readonly AppDbContext _db;
        public CouponRepo(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Coupon> GetByCodeAsync(string code)
        {
            return await (from coupon in _db.Coupons
                          where ( code.ToLower() == coupon.CouponCode.ToLower())
                          select new Coupon
                              {
                                  CouponCode = coupon.CouponCode,
                                  Id = coupon.Id,
                                  DiscountAmount = coupon.DiscountAmount,
                                  MinAmount = coupon.MinAmount
                              }
                          )
                          .FirstOrDefaultAsync();
        }
    }
}
