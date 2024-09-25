using Mango.Services.CouponApi.DataAccess.IRepos;
using Mango.Services.CouponApi.DataAccess.Repos;
using Mango.Services.CouponApi.IService;
using Mango.Services.CouponApi.Mapping;

namespace Mango.Services.CouponApi.Services
{
    public static class DependancyInjectionService
    {
        public static void AddDependancy(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CouponProfile));
            services.AddScoped(typeof(IGenericRepo<>),typeof(GenericRepo<>));
            services.AddScoped<ICouponRepo,CouponRepo>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICouponService, CouponService>();
        }
    }
}
