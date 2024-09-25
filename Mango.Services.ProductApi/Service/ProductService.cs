using AutoMapper;
using Mango.Services.ProductApi.Data;
using Mango.Services.ProductApi.Dto;
using Mango.Services.ProductApi.IService;
using Mango.Services.ProductApi.Model;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductApi.Service
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ProductService(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateAsync(ProductDto productDto)
        {
            var model = _mapper.Map<Product>(productDto);
            await _db.AddAsync(model);
            await _db.SaveChangesAsync();
            return productDto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return false;
            }
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<ProductDto?> GetProductById(int id)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var products = await _db.Products.ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto?> UpdateAsync(int id, ProductDto productDto)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            _mapper.Map(productDto, product);
            await _db.SaveChangesAsync();
            return productDto;
        }
    }
}
