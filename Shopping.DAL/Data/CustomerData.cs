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


        /// <summary>
        /// Register New Customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public static bool Register(CustomerDTO customer)
        {
            try
            {
                Customer dbCustomer = CustomerMapping.MapCustomer(customer);
                db.Customers.Add(dbCustomer);
                db.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new EmailAlreadyExists();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;

        }

        /// <summary>
        /// Find Customer
        /// </summary>
        /// <param name="customerDTO"></param>
        /// <returns></returns>
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
        public static CustomerDTO FindId(Guid? id)
        {
            try
            {
                var customerFound = db.Customers.FirstOrDefault(x => x.Id == id);
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

        public static CustomerDTO Edit(CustomerDTO customer)
        {
            try
            {              

                var foundCustomer = db.Customers.FirstOrDefault(x => x.Id == customer.Id);
                foundCustomer.Address1 = customer.Address1;
                foundCustomer.Address2 = customer.Address2;
                foundCustomer.Address3 = customer.Address3;
                db.SaveChanges();
                var foundCustomeragin = db.Customers.Find(customer.Id);
                CustomerDTO dbCustomer = CustomerMapping.MapCustomer(foundCustomeragin);
                return dbCustomer;
            }
           
            catch (Exception e)
            {
                return null;
            }
           

        }
    }
}
