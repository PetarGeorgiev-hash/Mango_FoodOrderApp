using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.ShoppingCartApi.Dto
{
    public class CartHeaderDto
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? CouponCode { get; set; }

        public double Discount { get; set; }

        public double CartTotal { get; set; }
    }
}
