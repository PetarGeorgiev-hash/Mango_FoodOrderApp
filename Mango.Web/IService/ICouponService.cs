using Mango.Web.Models;

namespace Mango.Web.IService
{
    public interface ICouponService
    {
        Task<ResponseDto?> GetCouponAsync(string couponCode);

        Task<ResponseDto?> GetAllCouponAsync();

        Task<ResponseDto?> GetCouponByIdAsync(int couponId);

        Task<ResponseDto?> CreateCouponAsync(ProductDto couponDto);

        Task<ResponseDto?> UpdateCouponAsync(ProductDto couponDto);

        Task<ResponseDto?> DeleteCouponAsync(int couponId);


    }
}
