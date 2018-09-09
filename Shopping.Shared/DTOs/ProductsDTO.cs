using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.DTOs
{
    public class ProductsDTO
    {
        public ProductsDTO()
        {
            this.Products = new List<ProductDTO>();
        }
        public List<ProductDTO> Products; 
    }
}
