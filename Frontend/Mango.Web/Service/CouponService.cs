using Mango.Web.IService;
using Mango.Web.Models;
using Mango.Web.Utilitiy;

namespace Mango.Web.Service
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;

        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CreateCouponAsync(ProductDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utilitiy.SD.ApiType.POST,
                Url = SD.CouponApiBase + "/api/coupon",
                Data = couponDto
            });
        }

        public async Task<ResponseDto?> DeleteCouponAsync(int couponId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utilitiy.SD.ApiType.DELETE,
                Url = SD.CouponApiBase + "/api/coupon" + "/" + couponId
            });
        }

        public async Task<ResponseDto?> GetAllCouponAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utilitiy.SD.ApiType.GET,
                Url = SD.CouponApiBase + "/api/coupon"
            });
        }

        public async Task<ResponseDto?> GetCouponAsync(string couponCode)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utilitiy.SD.ApiType.GET,
                Url = SD.CouponApiBase + "/api/coupon/GetByCode" + couponCode
            });
        }

        public async Task<ResponseDto?> GetCouponByIdAsync(int couponId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utilitiy.SD.ApiType.GET,
                Url = SD.CouponApiBase + "/api/coupon" + couponId
            });
        }

        public async Task<ResponseDto?> UpdateCouponAsync(ProductDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Url = SD.CouponApiBase + "/api/coupon",
                Data = couponDto
            });
        }
    }
}
