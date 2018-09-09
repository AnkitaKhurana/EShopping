using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.DTOs
{
    public class HomePageDTO
    {
        public HomePageDTO()
        {
            this.Trending = new List<TrendingDTO>();
        }
        public List<TrendingDTO> Trending;
        
    }
}
