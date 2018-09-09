﻿using Shopping.Data;
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


        public static ProductsDTO AllProducts(string searchString, Guid? categoryId)
        {
          
            try
            {
                ProductsDTO products = new ProductsDTO();
                var productRows = (from product in db.Products
                              select product);

                if(!String.IsNullOrEmpty(searchString))
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

                return products;
            }
            catch (Exception e)
            {
                return null;
            }

        }
             
    }
}