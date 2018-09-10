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

        /// <summary>
        /// Get top Categories from Analytics Table
        /// </summary>
        /// <returns></returns>
        public static ProductsDTO TopCategories()
        {
            try
            {
                ProductsDTO products = new ProductsDTO();
                var TopRows = (from product in db.TopProducts
                               select product).ToList();

                var orderByCategory = TopRows.OrderBy(x => x.ProductCategory.TotalSaleQuantity).ThenBy(y => y.TotalSale);

                foreach (var row in orderByCategory)
                {
                    List<string> variantList = new List<string>();
                    foreach (var variant in row.Product.ProductVariants)
                    {
                        variantList.Add(variant.Name);
                    }
                    products.Products.Add(new ProductDTO()
                    {
                        Id = row.Id,
                        ImageURL = row.Product.ImageURL,
                        Name = row.Product.Name,
                        Price = row.Product.Price,
                        Description = row.Product.Description,
                        TotalQuantitySale = row.Product.TotalQuantitySale,
                        Category = new CategoryDTO()
                        {
                            Id = row.ProductCategory.Id,
                            Name = row.ProductCategory.Name,
                            TotalSaleQuantity = row.ProductCategory.TotalSaleQuantity
                        },
                        Variants = variantList

                    });
                }
                return products;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get All Categories
        /// </summary>
        /// <returns></returns>
        public static CategoriesDTO AllCategories()
        {
            try
            {
                CategoriesDTO categories = new CategoriesDTO();
                var categoryRows = (from category in db.ProductCategories
                                    select category);

                foreach (var category in categoryRows)
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
