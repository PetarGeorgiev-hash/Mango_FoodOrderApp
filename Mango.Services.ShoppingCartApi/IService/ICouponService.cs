using Mango.Services.ShoppingCartApi.Dto;

namespace Mango.Services.ShoppingCartApi.IService
{
    public interface ICouponService
    {
        Task<CouponDto> GetCoupon(string couponCode);
    }
}
