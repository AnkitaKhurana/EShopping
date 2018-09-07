using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.DTOs
{
    public class ProductDTO
    {
        public ProductDTO()
        {
            this.CartLine = new HashSet<CartLineDTO>();
            this.OrderLine = new HashSet<OrderLineDTO>();
            this.ProductVariant = new HashSet<ProductVariantDTO>();
            this.TopProducts = new HashSet<TopProductDTO>();
        }

        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public System.Guid CategoryId { get; set; }
        public int TotalQuantitySale { get; set; }

        public virtual ICollection<CartLineDTO> CartLine { get; set; }
        public virtual ICollection<OrderLineDTO> OrderLine { get; set; }
        public virtual ProductCategoryDTO ProductCategory { get; set; }
        public virtual ICollection<ProductVariantDTO> ProductVariant { get; set; }
        public virtual ICollection<TopProductDTO> TopProducts { get; set; }
    }
}
