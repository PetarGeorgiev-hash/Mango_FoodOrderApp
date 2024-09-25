using AutoMapper;
using Mango.Services.CouponApi.DataAccess.IRepos;
using Mango.Services.CouponApi.Dto;
using Mango.Services.CouponApi.IService;
using Mango.Services.CouponApi.Models;

namespace Mango.Services.CouponApi.Services
{
    public class CouponService : ICouponService
    {
        private readonly ICouponRepo _repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CouponService(ICouponRepo repo, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CouponDto> Create(CouponDto coupon)
        {
            var model = _mapper.Map<Coupon>(coupon);
            var result = await _repo.CreateAsync(model);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CouponDto>(result);
        }

        public async Task<int> Delete(int id)
        {
            var coupon = await _repo.GetByIdAsync(id);
            if (coupon == null)
            {
                return 0;
            }
            await _repo.Delete(id);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<CouponDto> GetByCode(string code)
        {
            var coupon = await _repo.GetByCodeAsync(code);
            if (coupon == null)
            {
                throw new Exception("Coupon with this code does not exist");
            }
            return _mapper.Map<CouponDto>(coupon);
        }

        public async Task<CouponDto> GetById(int id)
        {
            var coupon = await _repo.GetByIdAsync(id);
            if (coupon == null)
            {
                throw new Exception("Coupon with this id does not exist");
            }
            return _mapper.Map<CouponDto>(coupon);
        }

        public async Task<IEnumerable<CouponDto>> GetCoupons()
        {
            var coupons = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<CouponDto>>(coupons);
        }

        public async Task<CouponDto> Update(int id, CouponDto coupon)
        {
            var model = await _repo.GetByIdAsync(id);
            if(model == null)
            {
                throw new Exception("Coupon with this id does not exist");
            }
            var result = _mapper.Map(coupon, model);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CouponDto>(result);
        }
    }
}
