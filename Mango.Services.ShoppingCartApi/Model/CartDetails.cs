using Mango.Services.ShoppingCartApi.Dto;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.ShoppingCartApi.Model
{
    public class CartDetails
    {
        public int Id { get; set; }
        public int CartHeaderId { get; set; }
        [ForeignKey("CartHeaderId")]
        public CartHeader CartHeader { get; set; }
        public int ProductId { get; set; }

        [NotMapped]
        public ProductDto Product { get; set; }
        public int Count { get; set; }
         
    }
}
