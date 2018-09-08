using Shopping.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping.DAL.Map;
using Shopping.Data;


namespace Shopping.DAL.Data
{
    public class CustomerData
    {
        private static ShoppingDatabaseEntities db = new ShoppingDatabaseEntities();


        public static bool Register(CustomerDTO customer)
        {
            try
            {
                Customer dbCustomer = CustomerMapping.MapCustomer(customer);
                db.Customers.Add(dbCustomer);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;

        }

        public static CustomerDTO Find(CustomerDTO customerDTO)
        {
            try
            {
                var customerFound = db.Customers.FirstOrDefault(x => x.Email == customerDTO.Email && x.Password == customerDTO.Password);
                if (customerFound != null)
                {
                    CustomerDTO resultCustomer = CustomerMapping.MapCustomer(customerFound);
                    return resultCustomer;
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public static CartDTO MyCart(Guid? id)
        {
            try
            {
                var cartjoin = (from c in db.Customers
                                join cl in db.CartLines on c.Id equals cl.CustomerId
                                join p in db.Products on cl.ProductId equals p.Id
                                join pc in db.ProductCategories on p.CategoryId equals pc.Id
                                select new
                                {
                                    c,
                                    cl,
                                    p,
                                    pc
                                }).ToList();

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
                        Quantity = result.cl.Quantity,
                        product = new ProductDTO()
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
        

    }
}
