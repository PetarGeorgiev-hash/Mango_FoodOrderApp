using Mango.Services.CouponApi.Models;

namespace Mango.Services.CouponApi.DataAccess.IRepos
{
    public interface ICouponRepo : IGenericRepo<Coupon>
    {
        public Task<Coupon?> GetByCodeAsync(string code);
    }
}
