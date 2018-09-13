using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.DTOs
{
    public class CartItemDTO
    {
        public Guid? Id { get; set; }
        public ProductDTO Product { get; set; }
        public int Quantity { get; set; }
        public string VariantName { get; set; }
    }
}
