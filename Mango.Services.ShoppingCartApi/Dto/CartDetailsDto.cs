using Mango.Services.ShoppingCartApi.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.ShoppingCartApi.Dto
{
    public class CartDetailsDto
    {
        public int Id { get; set; }
        public int CartHeaderId { get; set; }
        public CartHeaderDto? CartHeader { get; set; }
        public int ProductId { get; set; }
        public ProductDto? Product { get; set; }
        public int Count { get; set; }
    }
}
