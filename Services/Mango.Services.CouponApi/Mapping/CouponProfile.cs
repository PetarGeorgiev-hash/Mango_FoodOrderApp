using AutoMapper;
using Mango.Services.CouponApi.Dto;
using Mango.Services.CouponApi.Models;

namespace Mango.Services.CouponApi.Mapping
{
    public class CouponProfile : Profile
    {
        public CouponProfile()
        {
            CreateMap<CouponDto, Coupon>();
            CreateMap<Coupon, CouponDto>();
        }
    }
}
