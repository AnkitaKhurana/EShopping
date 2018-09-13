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
            bool analyticsUpdateStatus = false;
            try
            {
                // Data Updation and retrieval  
                var analytics = (from a in db.TopProducts
                                 select a).ToList();
                var categories = (from c in db.ProductCategories
                                  select c).ToList();
                foreach (var orderItem in order.orders)
                {
                    var totalsale = db.Products.FirstOrDefault(x => x.Id == orderItem.ProductId).TotalQuantitySale += orderItem.Quantity;
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
                foreach (var orderItem in newOrdersPlaced)
                {
                    var existingEntry = db.TopProducts.FirstOrDefault(x => x.ProductId == orderItem.ProductId);
                    if (existingEntry != null)
                    {
                        existingEntry.TotalSale += orderItem.Quantity;
                    }
                    db.SaveChanges();
                }

                // For each category check the analytics and Orders
                foreach (var category in categories)
                {
                    {
                        var categoryRowsInAnalytics = analytics.Where(x => x.ProductCategoryId == category.Id);
                        if (categoryRowsInAnalytics.Count() < 5)
                        {
                            Guid categoryId = category.Id;
                            var topProductsInOrder = (from o in db.OrderLines
                                                      where o.OrderId == order.Id &&
                                                      o.Product.ProductCategory.Id == categoryId
                                                      select o).AsEnumerable();

                            int totalCount = topProductsInOrder.Count() - 1;
                            while (categoryRowsInAnalytics.Count() != 5 && totalCount >= 0)
                            {
                                Guid id = topProductsInOrder.ElementAt(totalCount).ProductId;
                                var found = db.TopProducts.FirstOrDefault(x => x.ProductId == id);

                                if (found == null)
                                {
                                    var newProduct = new TopProduct()
                                    {
                                        Id = Guid.NewGuid(),
                                        TotalSale = topProductsInOrder.ElementAt(totalCount).Product.TotalQuantitySale,
                                        ProductId = topProductsInOrder.ElementAt(totalCount).ProductId,
                                        ProductCategoryId = topProductsInOrder.ElementAt(totalCount).Product.ProductCategory.Id,
                                    };
                                    db.TopProducts.Add(newProduct);
                                    db.SaveChanges();
                                    analytics = (from a in db.TopProducts
                                                 select a).ToList();
                                    categoryRowsInAnalytics = analytics.Where(x => x.ProductCategoryId == category.Id);
                                }

                                totalCount--;
                            }
                        }
                        else
                        {
                            var topProductsInOrder = newOrdersPlaced.Where(x => x.Product.CategoryId == category.Id);
                            foreach (var orderItem in topProductsInOrder)
                            {
                                foreach (var analyticItem in categoryRowsInAnalytics)
                                {
                                    if (analyticItem.TotalSale <= orderItem.Product.TotalQuantitySale)
                                    {
                                        var found = (from s in db.TopProducts
                                                     where s.ProductId == orderItem.ProductId
                                                     select s).FirstOrDefault();

                                        if (found == null)
                                        {
                                            db.TopProducts.Remove(analyticItem);
                                            db.TopProducts.Add(new TopProduct()
                                            {
                                                Id = Guid.NewGuid(),
                                                ProductCategoryId = orderItem.Product.CategoryId,
                                                ProductId = orderItem.ProductId,
                                                TotalSale = orderItem.Product.TotalQuantitySale
                                            });
                                            db.SaveChanges();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                analyticsUpdateStatus = true;
            }
            catch (Exception e)
            {
                analyticsUpdateStatus = false;
            }
            return analyticsUpdateStatus;

        }

        /// <summary>
        /// Get top trending Products
        /// </summary>
        /// <returns></returns>
        public static ProductsDTO GetTopProducts()
        {
            ProductsDTO products = new ProductsDTO();
            try
            {
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
            }
            catch
            {
                products = null;
            }
            return products;
        }

    }
}
