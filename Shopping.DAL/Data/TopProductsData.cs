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

        public static bool UpdateTopProducts(OrderDTO order)
        {
            try
            {
                var analytics = (from a in db.TopProducts
                                 select a).ToList();
                var categories = (from c in db.ProductCategories
                                  select c).ToList(); ;
                foreach(var orderItem in order.orders)
                {
                    db.Products.FirstOrDefault(x => x.Id == orderItem.ProductId).TotalQuantitySale += orderItem.Quantity;
                    db.SaveChanges();    
                }
                
                var newOrdersPlaced = (from o in db.OrderLines
                                       where o.OrderId == order.Id
                                       select o).ToList(); 
                foreach (var category in categories)
                {
                    var categoryRowsInAnalytics = analytics.Where(x => x.ProductCategoryId == category.Id);
                    if (categoryRowsInAnalytics.Count() < 5)
                    {
                        var topProductsInOrder = newOrdersPlaced.Where(x=> x.Product.ProductCategory.Id==category.Id);
                        int i = topProductsInOrder.Count()-1;
                        while (categoryRowsInAnalytics.Count() != 5 && i>=0)
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
                        foreach(var orderItem in topProductsInOrder)
                        {
                            foreach(var analyticItem in categoryRowsInAnalytics)
                            {
                                if(analyticItem.TotalSale < orderItem.Product.TotalQuantitySale)
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
    }
}
