using Mango.Services.CouponApi.Dto;

namespace Mango.Services.CouponApi.IService
{
    public interface ICouponService
    {
        Task<IEnumerable<CouponDto>> GetCoupons();

        Task<CouponDto?> GetById(int id);

        Task<CouponDto?> GetByCode(string code);

        Task<CouponDto> Create(CouponDto coupon);

        Task<CouponDto?> Update(int id,CouponDto coupon);

        Task<int> Delete(int id);
    }
}
