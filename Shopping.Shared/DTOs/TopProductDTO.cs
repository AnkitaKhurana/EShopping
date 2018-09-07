using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.DTOs
{
    public class TopProductDTO
    {
        public System.Guid Id { get; set; }
        public int TotalSale { get; set; }
        public System.Guid ProductId { get; set; }
        public System.Guid ProductCategoryId { get; set; }

        public virtual ProductDTO Product { get; set; }
        public virtual ProductCategoryDTO ProductCategory { get; set; }
    }
}
