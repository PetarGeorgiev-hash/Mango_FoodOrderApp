using Mango.Web.Models;

namespace Mango.Web.IService
{
    public interface IProductService
    {

        Task<ResponseDto?> GetAllProductsAsync();

        Task<ResponseDto?> GetProductByIdAsync(int productId);

        Task<ResponseDto?> CreateProductAsync(ProductDto couponDto);

        Task<ResponseDto?> UpdateProductAsync(ProductDto couponDto);

        Task<ResponseDto?> DeleteProductAsync(int productId);


    }
}
