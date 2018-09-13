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

        /// <summary>
        /// Get All products 
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public static ProductsDTO AllProducts(string searchString, Guid? categoryId)
        {
            ProductsDTO products = new ProductsDTO();
            try
            {
                var productRows = (from product in db.Products
                                   select product);

                if (!String.IsNullOrEmpty(searchString))
                {
                    productRows = productRows.Where(s => s.Name.Contains(searchString.ToString()) || s.Description.Contains(searchString.ToString()));
                }
                if (categoryId.HasValue)
                {
                    productRows = productRows.Where(s => s.CategoryId == categoryId);
                }

                foreach (var row in productRows)
                {
                    List<string> variantList = new List<string>();
                    foreach (var variant in row.ProductVariants)
                    {
                        variantList.Add(variant.Name);
                    }
                    products.Products.Add(new ProductDTO()
                    {
                        Id = row.Id,
                        ImageURL = row.ImageURL,
                        Name = row.Name,
                        Price = row.Price,
                        Description = row.Description,
                        TotalQuantitySale = row.TotalQuantitySale,
                        Category = new CategoryDTO()
                        {
                            Id = row.ProductCategory.Id,
                            Name = row.ProductCategory.Name,
                            TotalSaleQuantity = row.ProductCategory.TotalSaleQuantity
                        },
                        Variants = variantList
                    });
                }
            }
            catch (Exception e)
            {
                products = null;
            }
            return products;

        }

        /// <summary>
        /// Get Product Details 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static ProductDTO ProductDetail(Guid productId)
        {
            ProductDTO productDTO = null;
            try
            {
                var product = (from p in db.Products
                               where p.Id == productId
                               select p).FirstOrDefault();

                if (product != null)
                {
                    List<string> variantList = new List<string>();

                    foreach (var variant in product.ProductVariants)
                    {
                        variantList.Add(variant.Name);
                    }
                    productDTO = new ProductDTO()
                    {
                        Id = product.Id,
                        ImageURL = product.ImageURL,
                        Name = product.Name,
                        Price = product.Price,
                        Description = product.Description,
                        TotalQuantitySale = product.TotalQuantitySale,
                        Category = new CategoryDTO()
                        {
                            Id = product.ProductCategory.Id,
                            Name = product.ProductCategory.Name,
                            TotalSaleQuantity = product.ProductCategory.TotalSaleQuantity
                        },
                        Variants = variantList
                    };
                }
            }
            catch
            {
                productDTO = null;
            }
            return productDTO;
        }
    }
}
