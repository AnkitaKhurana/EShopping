using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.DTOs
{
    public class TrendingDTO
    {
        public TrendingDTO()
        {
            this.TopProducts = new List<ProductsDTO>();
        }
        public List<ProductsDTO> TopProducts{get;set;}
        public CategoryDTO Category;
        public int Rank;
    }
}
