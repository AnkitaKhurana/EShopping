using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.DTOs
{
    public class CategoriesDTO
    {
        public CategoriesDTO()
        {
            this.categories = new List<CategoryDTO>();
        }
        public List<CategoryDTO> categories;
    }
}
