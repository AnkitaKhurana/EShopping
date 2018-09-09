using Shopping.Data;
using Shopping.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.DAL.Data
{
    public class CategoryData
    {
        private static ShoppingDatabaseEntities db = new ShoppingDatabaseEntities();

        public static CategoriesDTO AllCategories()
        {
            try
            {
                CategoriesDTO categories = new CategoriesDTO();
                var categoryRows = (from category in db.ProductCategories
                                   select category);

               foreach(var category in categoryRows)
                {
                    categories.categories.Add(new CategoryDTO()
                    {
                        Id = category.Id,
                        Name = category.Name,
                        TotalSaleQuantity = category.TotalSaleQuantity                        
                    });
                }
                return categories;
            }
            catch
            {
                return null;
            }
        }
    }
}
