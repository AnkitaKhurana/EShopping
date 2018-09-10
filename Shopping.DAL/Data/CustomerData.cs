using Shopping.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping.DAL.Map;
using Shopping.Data;
using System.Data.Entity.Infrastructure;
using Shopping.Shared.Exceptions;

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
            catch(DbUpdateException e)
            {
                throw new EmailAlreadyExists();
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
               
    }
}
