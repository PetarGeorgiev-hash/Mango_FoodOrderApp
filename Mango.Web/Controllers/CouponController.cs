using Mango.Web.IService;
using Mango.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

namespace Mango.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto> coupons = new();
            var response = await _couponService.GetAllCouponAsync();

            if (response != null && response.IsSuccess)
            {
                coupons = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
                TempData["success"] = "Got all the coupons";
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(coupons);
        }

        public async Task<IActionResult> CouponCreate(CouponDto coupon)
        {
            if (ModelState.IsValid)
            {
                var response = await _couponService.CreateCouponAsync(coupon);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Coupon Created";
                    return RedirectToAction("CouponIndex");
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(coupon);
        }

        public async Task<IActionResult> CouponDelete(int id)
        {
            var response = await _couponService.DeleteCouponAsync(id);
            if(response != null && response.IsSuccess)
            {
                TempData["success"] = "Coupon Deleted";
                return RedirectToAction("CouponIndex");
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return NotFound(id);
        }
    }
}
