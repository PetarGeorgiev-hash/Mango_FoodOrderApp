using AutoMapper;
using Mango.Services.ProductApi.Dto;
using Mango.Services.ProductApi.Model;

namespace Mango.Services.ProductApi.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();

        }
    }
}
