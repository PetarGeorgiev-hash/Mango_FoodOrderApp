using AutoMapper;
using Mango.Services.ShoppingCartApi.Dto;
using Mango.Services.ShoppingCartApi.Model;

namespace Mango.Services.ShoppingCartApi.Profiles
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
            CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
        }
    }
}
