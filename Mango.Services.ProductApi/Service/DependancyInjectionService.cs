using Mango.Services.ProductApi.IService;
using Mango.Services.ProductApi.Profiles;

namespace Mango.Services.ProductApi.Service
{
    public static class DependancyInjectionService
    {
        public static void DIConfigure(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ProductProfile));
            services.AddScoped<IProductService, ProductService>();
        }
    }
}
