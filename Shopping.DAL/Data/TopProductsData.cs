using Shopping.Data;
using Shopping.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.DAL.Data
{
    public class TopProductsData
    {
        private static ShoppingDatabaseEntities db = new ShoppingDatabaseEntities();


        /// <summary>
        /// Update Analytics Table
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public static bool UpdateTopProducts(OrderDTO order)
        {
            try
            {
                var analytics = (from a in db.TopProducts
                                 select a).ToList();
                var categories = (from c in db.ProductCategories
                                  select c).ToList();

                foreach (var orderItem in order.orders)
                {
                    db.Products.FirstOrDefault(x => x.Id == orderItem.ProductId).TotalQuantitySale += orderItem.Quantity;
                    db.SaveChanges();
                }

                var newOrdersPlaced = (from o in db.OrderLines
                                       where o.OrderId == order.Id
                                       select o).ToList();

                foreach (var orderItem in newOrdersPlaced)
                {
                    db.ProductCategories.FirstOrDefault(x => x.Id == orderItem.Product.CategoryId).TotalSaleQuantity += orderItem.Quantity;
                    db.SaveChanges();
                }

                foreach (var category in categories)
                {
                    var categoryRowsInAnalytics = analytics.Where(x => x.ProductCategoryId == category.Id);
                    if (categoryRowsInAnalytics.Count() < 5)
                    {
                        var topProductsInOrder = newOrdersPlaced.Where(x => x.Product.ProductCategory.Id == category.Id);
                        int i = topProductsInOrder.Count() - 1;
                        while (categoryRowsInAnalytics.Count() != 5 && i >= 0)
                        {
                            var newProduct = new TopProduct()
                            {
                                Id = Guid.NewGuid(),
                                TotalSale = topProductsInOrder.ElementAt(i).Product.TotalQuantitySale,
                                ProductId = topProductsInOrder.ElementAt(i).ProductId,
                                ProductCategoryId = topProductsInOrder.ElementAt(i).Product.ProductCategory.Id,
                            };
                            db.TopProducts.Add(newProduct);
                            db.SaveChanges();
                            analytics = (from a in db.TopProducts
                                         select a).ToList();
                            categoryRowsInAnalytics = analytics.Where(x => x.ProductCategoryId == category.Id);
                            i--;
                        }
                    }
                    else
                    {
                        var topProductsInOrder = newOrdersPlaced.OrderBy(x => x.Quantity).Where(x => x.Product.ProductCategory.Id == category.Id);
                        foreach (var orderItem in topProductsInOrder)
                        {
                            foreach (var analyticItem in categoryRowsInAnalytics)
                            {
                                if (analyticItem.TotalSale < orderItem.Product.TotalQuantitySale)
                                {
                                    db.TopProducts.Remove(analyticItem);
                                    db.TopProducts.Add(new TopProduct()
                                    {
                                        Id = Guid.NewGuid(),
                                        ProductCategoryId = analyticItem.ProductCategoryId,
                                        ProductId = orderItem.ProductId,
                                        TotalSale = orderItem.Product.TotalQuantitySale
                                    });
                                    db.SaveChanges();
                                }
                            }
                        }


                    }

                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        /// <summary>
        /// Get top trending Products
        /// </summary>
        /// <returns></returns>
        public static ProductsDTO GetTopProducts()
        {
            try
            {
                ProductsDTO products = new ProductsDTO();
                var topProducts = (from a in db.TopProducts
                                   select a).ToList();
                foreach (var product in topProducts)
                {
                    List<string> varinats = new List<string>();
                    foreach (var variant in product.Product.ProductVariants)
                    {
                        varinats.Add(variant.Name);
                    }
                    products.Products.Add(new ProductDTO()
                    {
                        Id = product.Id,
                        Name = product.Product.Name,
                        Description = product.Product.Description,
                        Price = product.Product.Price,
                        ImageURL = product.Product.ImageURL,
                        TotalQuantitySale = product.Product.TotalQuantitySale,
                        Variants = varinats,
                        Category = new CategoryDTO()
                        {
                            Id = product.ProductCategoryId,
                            Name = product.ProductCategory.Name,
                            TotalSaleQuantity = product.ProductCategory.TotalSaleQuantity
                        }

                    });
                }

                return products;
            }
            catch
            {
                return null;
            }
        }

    }
}
