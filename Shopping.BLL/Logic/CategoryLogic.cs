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
        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns></returns>
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
