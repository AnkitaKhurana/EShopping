using Shopping.Data;
using Shopping.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.DAL.Data
{
    public class CartData
    {
        private static ShoppingDatabaseEntities db = new ShoppingDatabaseEntities();


        public static CartDTO Cart(Guid id)
        {
            try
            {

                var cartjoin = (from customer in db.Customers
                                where customer.Id == id
                                join cl in db.CartLines on customer.Id equals cl.CustomerId
                                join p in db.Products on cl.ProductId equals p.Id
                                join pc in db.ProductCategories on p.CategoryId equals pc.Id
                                select new { cl, p, pc }).ToList();

                CartDTO cartDTO = new CartDTO();
                foreach (var result in cartjoin)
                {
                    List<String> variants = new List<String>();
                    foreach (var v in result.p.ProductVariants)
                    {
                        variants.Add(v.Name);
                    }

                    CartItemDTO cartLineDTO = new CartItemDTO()
                    {
                        Id = result.cl.Id,
                        Quantity = result.cl.Quantity,
                        Product = new ProductDTO()
                        {
                            Id = result.p.Id,
                            Name = result.p.Name,
                            Price = result.p.Price,
                            Description = result.p.Description,
                            ImageURL = result.p.ImageURL,
                            TotalQuantitySale = result.p.TotalQuantitySale,
                            Category = result.pc.Name,
                            Variants = variants
                        }

                    };
                    cartDTO.items.Add(cartLineDTO);
                }
                return cartDTO;
            }
            catch
            {
                return null;
            }
        }


        public static CartDTO Edit(Guid CustomerId, Guid ProductId, int quantity)
        {
            try
            {

                CartDTO cartDTO = new CartDTO();
                var alreadyExistingRecord = db.CartLines.Where(x => x.CustomerId == CustomerId && x.ProductId == ProductId);
                if (alreadyExistingRecord.Count() != 0)
                {
                    alreadyExistingRecord.FirstOrDefault().Quantity = quantity ;
                    db.SaveChanges();
                }
                else
                {
                    CartLine cartLineDTO = new CartLine()
                    {
                        CustomerId = CustomerId,
                        ProductId = ProductId,
                        Quantity = quantity
                    };

                    db.CartLines.Add(cartLineDTO);
                }
                cartDTO = Cart(CustomerId);
                return cartDTO;
            }
            catch
            {
                return null;
            }
        }
    }
}
