using Mango.Services.ProductApi.Dto;

namespace Mango.Services.ProductApi.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<ProductDto> CreateAsync(ProductDto productDto);
        Task<ProductDto?> UpdateAsync(int id,ProductDto productDto);
        Task<bool> DeleteAsync(int id);
    }
}
