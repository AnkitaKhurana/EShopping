using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.DTOs
{
    public class ProductVariantDTO
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public System.Guid ProductId { get; set; }

        public virtual ProductDTO Product { get; set; }
    }
}
