using Shopping.DAL.Data;
using Shopping.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.BLL.Logic
{
    public class CategoryLogic
    {
        public static CategoriesDTO AllCategories()
        {
            try
            {
                CategoriesDTO categories = CategoryData.AllCategories();
                return categories;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
