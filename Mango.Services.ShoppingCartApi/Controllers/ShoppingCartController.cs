using AutoMapper;
using Mango.Services.ShoppingCartApi.Data;
using Mango.Services.ShoppingCartApi.Dto;
using Mango.Services.ShoppingCartApi.IService;
using Mango.Services.ShoppingCartApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ShoppingCartApi.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private ResponseDto _responseDto;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;
        private readonly ICouponService _couponService;

        public ShoppingCartController(IMapper mapper, ApplicationDbContext context, IProductService productService, ICouponService couponService)
        {
            _responseDto = new();
            _mapper = mapper;
            _context = context;
            _productService = productService;
            _couponService = couponService;
        }

        [HttpGet("GetCart/{userId}")]
        public async Task<ResponseDto> GetCart([FromRoute] string userId)
        {
            try
            {
                CartDto cartDto = new()
                {
                    CartHeader = _mapper.Map<CartHeaderDto>(_context.CartHeaders.First(u => u.UserId == userId)),
                };
                cartDto.CartDetails = _mapper.Map<IEnumerable<CartDetailsDto>>(_context.CartDetails
                    .Where(u => u.CartHeaderId == cartDto.CartHeader.Id));

                IEnumerable<ProductDto> products = await _productService.GetProducts();
                foreach (var item in cartDto.CartDetails)
                {
                    item.Product = products.FirstOrDefault(u => u.Id == item.ProductId);
                    cartDto.CartHeader.CartTotal += item.Count * item.Product.Price;
                }
                if (!string.IsNullOrEmpty(cartDto.CartHeader.CouponCode))
                {
                    CouponDto couponDto = await _couponService.GetCoupon(cartDto.CartHeader.CouponCode);
                    if (couponDto != null && cartDto.CartHeader.CartTotal > couponDto.MinAmount)
                    {
                        cartDto.CartHeader.CartTotal -= couponDto.DiscountAmount;
                        cartDto.CartHeader.Discount = couponDto.DiscountAmount;
                    }
                }
                _responseDto.Result = cartDto;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpPost("ApplyCoupon")]
        public async Task<object> ApplyCoupon([FromBody] CartDto cartDto)
        {
            try 
            {
                var cartFromDb = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == cartDto.CartHeader.UserId);
                cartFromDb.CouponCode = cartDto.CartHeader?.CouponCode;
                _context.CartHeaders.Update(cartFromDb);
                await _context.SaveChangesAsync();
                _responseDto.Result = true;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }

            return _responseDto;
        }

        [HttpPost("RemoveCoupon")]
        public async Task<object> RemoveCoupon([FromBody] CartDto cartDto)
        {
            try
            {
                var cartFromDb = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == cartDto.CartHeader.UserId);
                cartFromDb.CouponCode = "";
                _context.CartHeaders.Update(cartFromDb);
                await _context.SaveChangesAsync();
                _responseDto.Result = true;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }

            return _responseDto;
        }

        [HttpPost("CartUpsert")]
        public async Task<ResponseDto> CartUpsert([FromBody] CartDto cartDto)
        {
            try
            {
                var cartHeader = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(c => c.UserId == cartDto.CartHeader.UserId);
                if (cartHeader == null)
                {
                    var cartHeaderModel = _mapper.Map<CartHeader>(cartDto.CartHeader);
                    await _context.CartHeaders.AddAsync(cartHeaderModel);
                    await _context.SaveChangesAsync();
                    cartDto.CartDetails.First().CartHeaderId = cartHeaderModel.Id;
                    await _context.CartDetails.AddAsync(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var cartDetails = await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(c => c.ProductId == cartDto.CartDetails.First().ProductId && c.CartHeaderId == cartHeader.Id);
                    if (cartDetails == null)
                    {
                        cartDto.CartDetails.First().CartHeaderId = cartHeader.Id;
                        await _context.CartDetails.AddAsync(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        cartDto.CartDetails.First().Count += cartDetails.Count;
                        cartDto.CartDetails.First().CartHeaderId = cartDetails.CartHeaderId;
                        cartDto.CartDetails.First().Id = cartDetails.Id;
                        _context.CartDetails.Update(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                        await _context.SaveChangesAsync();
                    }
                }
                _responseDto.Result = cartDto;
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message.ToString();
                _responseDto.IsSuccess = false;
            }
            return _responseDto;
        }


        [HttpPost("RemoveCart")]
        public async Task<ResponseDto> RemoveCart([FromBody] int cartDetailsId)
        {
            try
            {
                CartDetails cartDetails = await _context.CartDetails.FirstOrDefaultAsync(c => c.Id == cartDetailsId);

                int totalCount = _context.CartDetails.Where(u => u.CartHeaderId == cartDetails.CartHeaderId).Count();
                _context.CartDetails.Remove(cartDetails);
                if (totalCount == 1)
                {
                    var cartHeaderToRemove = await _context.CartHeaders.FirstOrDefaultAsync(u => u.Id == cartDetails.CartHeaderId);
                    _context.CartHeaders.Remove(cartHeaderToRemove);
                }
                await _context.SaveChangesAsync();
                _responseDto.Result = true;
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message.ToString();
                _responseDto.IsSuccess = false;
            }
            return _responseDto;
        }
    }
}
