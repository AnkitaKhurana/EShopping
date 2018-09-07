using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Shopping.Shared.DTOs
{
    public class ProductCategoryDTO
    {
        public ProductCategoryDTO()
        {
            this.Product = new HashSet<ProductDTO>();
            this.TopProducts = new HashSet<TopProductDTO>();
        }

        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public int TotalSaleQuantity { get; set; }

        public virtual ICollection<ProductDTO> Product { get; set; }
        public virtual ICollection<TopProductDTO> TopProducts { get; set; }
    }
}
