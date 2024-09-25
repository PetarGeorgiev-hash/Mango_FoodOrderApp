using Mango.Web.IService;
using Mango.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto> products = new();
            var response = await _productService.GetAllProductsAsync();

            if (response != null && response.IsSuccess)
            {
                products = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
                TempData["success"] = "Got all the products";
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(products);
        }

        public async Task<IActionResult> ProductCreate(ProductDto product)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProductAsync(product);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Product Created";
                    return RedirectToAction("ProductIndex");
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(product);
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await _productService.DeleteProductAsync(id);
            if(response != null && response.IsSuccess)
            {
                TempData["success"] = "Product Deleted";
                return RedirectToAction(nameof(ProductIndex));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return NotFound(id);
        }

        public async Task<IActionResult> ProductEdit()
    }
}
