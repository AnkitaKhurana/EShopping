using Shopping.Data;
using Shopping.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.DAL.Data
{
    public class ProductData
    {
        private static ShoppingDatabaseEntities db = new ShoppingDatabaseEntities();


        public static ProductsDTO AllProducts()
        {
            try
            {
                ProductsDTO products = new ProductsDTO();
                var productRows =  (from product in db.Products                                       
                                    select new { product }).ToList();

                foreach (var row in productRows)
                {
                    List<string> variantList = new List<string>();
                    foreach(var variant in row.product.ProductVariants)
                    {
                        variantList.Add(variant.Name);
                    }
                    products.Products.Add(new ProductDTO()
                    {
                        Id = row.product.Id,
                        ImageURL = row.product.ImageURL,
                        Name = row.product.Name,
                        Price = row.product.Price,
                        Description = row.product.Description,
                        TotalQuantitySale = row.product.TotalQuantitySale,
                        Category = row.product.ProductCategory.Name,
                        Variants = variantList

                    });
                }

                return products;
            }
            catch (Exception e)
            {
                return null;
            }
            
        }

    }
}
