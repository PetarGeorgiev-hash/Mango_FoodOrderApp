using Mango.Services.CouponApi.Dto;
using Mango.Services.CouponApi.IService;
using Mango.Services.CouponApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Azure.Core.HttpHeader;

namespace Mango.Services.CouponApi.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _couponService;
        private ResponseDto _responseDto;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
            _responseDto = new ResponseDto();
        }

        [HttpGet]
        public async Task<ResponseDto> Get()
        {
            try
            {
                _responseDto.Result = await _couponService.GetCoupons();
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.IsSuccess = false;
            }
            return _responseDto;
        }


        [HttpGet("{id}")]
        public async Task<ResponseDto> GetById([FromRoute] int id)
        {
            try
            {
                _responseDto.Result = await _couponService.GetById(id);
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.IsSuccess = false;
            }
            return _responseDto;
        }

        [HttpGet("GetByCode/{code}")]
        public async Task<ResponseDto> GetByCode([FromRoute] string code)
        {
            try
            {
                _responseDto.Result = await _couponService.GetByCode(code);
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.IsSuccess = false;
            }
            return _responseDto;
        }

        [HttpPost]
        public async Task<ResponseDto> CreateCoupon([FromBody] CouponDto coupon)
        {
            try
            {
                _responseDto.Result = await _couponService.Create(coupon);
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.IsSuccess = false;
            }
            return _responseDto;
        }

        [HttpPut]
        public async Task<ResponseDto> UpdateCoupon([FromBody] CouponDto coupon)
        {
            try
            {
                _responseDto.Result = await _couponService.Update(coupon.Id, coupon);
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.IsSuccess = false;
            }
            return _responseDto;
        }

        [HttpDelete("{id}")]
        public async Task<ResponseDto> DeleteCoupon([FromRoute] int id)
        {
            try
            {
                _responseDto.Result = await _couponService.Delete(id);
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.IsSuccess = false;
            }
            return _responseDto;
        }
    }
}
