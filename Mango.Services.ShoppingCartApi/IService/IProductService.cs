using Mango.Services.ShoppingCartApi.Dto;

namespace Mango.Services.ShoppingCartApi.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
