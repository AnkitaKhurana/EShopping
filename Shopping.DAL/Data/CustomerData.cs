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
        private static ShoppingDataEntities db = new ShoppingDataEntities();


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
                if (customerFound!=null)
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

        //public static  MyCart(Guid? id)
        //{
        //    try
        //    {
        //        //var cart = db.CartLine.Find();
        //        var cartProducts = (from ct in db.Cart
        //                    from c in db.Customer.Where(x => x.Cart == ).DefaultIfEmpty()
        //                    from p in db.Phones.Where(x => x.id_contact == c.id).DefaultIfEmpty()
        //                    from t in db.Tags.Where(x => x.id_contact == c.id).DefaultIfEmpty()
        //                    select new
        //                    {
        //                        id = c.id,
        //                        phones = p.number,
        //                        emails = e.email1,
        //                        tags = t.tag1,
        //                        firstname = c.firstname,
        //                        lastname = c.lastname,
        //                        address = c.address,
        //                        city = c.city,
        //                        bookmarked = c.bookmarked,
        //                        notes = c.notes
        //                    }).ToList();

        //        if (cart != null)
        //        {
        //            return cart;
        //        }
        //        return null;
        //    }
        //    catch (Exception e)
        //    {
        //        return null;
        //    }
        //}
      
    }
}
