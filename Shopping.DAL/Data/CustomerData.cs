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
            bool registrationSuccess = false;
            try
            {
                Customer dbCustomer = CustomerMapping.MapCustomer(customer);

                var alreadyExistsCheck = db.Customers.Find(customer.Id);
                if (alreadyExistsCheck != null)
                {
                    db.Customers.Add(dbCustomer);
                    db.SaveChanges();
                }
                else
                {
                    throw new EmailAlreadyExists();
                }
                registrationSuccess = true;

            }
            catch (EmailAlreadyExists)
            {
                throw new EmailAlreadyExists();
            }
            catch (Exception e)
            {
                registrationSuccess = false;
            }
            return registrationSuccess;
        }

        /// <summary>
        /// Find Customer
        /// </summary>
        /// <param name="customerDTO"></param>
        /// <returns></returns>
        public static CustomerDTO Find(CustomerDTO customerDTO)
        {
            CustomerDTO resultCustomer = null;
            try
            {
                var customerFound = db.Customers.FirstOrDefault(x => x.Email == customerDTO.Email && x.Password == customerDTO.Password);
                if (customerFound != null)
                {
                    resultCustomer = CustomerMapping.MapCustomer(customerFound);
                }
                else
                {
                    throw new NoSuchUserFound();
                }
            }
            catch (NoSuchUserFound)
            {
                throw new NoSuchUserFound();
            }
            catch (Exception e)
            {
                resultCustomer = null;
            }
            return resultCustomer;
        }


        /// <summary>
        /// Find Customer 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CustomerDTO FindId(Guid? id)
        {
            CustomerDTO resultCustomer = null;
            try
            {
                var customerFound = db.Customers.FirstOrDefault(x => x.Id == id);
                if (customerFound != null)
                {
                    resultCustomer = CustomerMapping.MapCustomer(customerFound);
                    return resultCustomer;
                }
                else
                {
                    throw new NoSuchUserFound();
                }
            }
            catch (NoSuchUserFound)
            {
                throw new NoSuchUserFound();
            }
            catch (Exception e)
            {
                resultCustomer = null;
            }
            return resultCustomer;
        }

        /// <summary>
        /// Edit Customer of customer Id to update Address
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public static CustomerDTO Edit(CustomerDTO customer)
        {
            CustomerDTO dbCustomer = null;
            try
            {
                var foundCustomer = db.Customers.FirstOrDefault(x => x.Id == customer.Id);
                foundCustomer.Address1 = customer.Address1;
                foundCustomer.Address2 = customer.Address2;
                foundCustomer.Address3 = customer.Address3;
                db.SaveChanges();
                var foundCustomeragin = db.Customers.Find(customer.Id);
                dbCustomer = CustomerMapping.MapCustomer(foundCustomeragin);
            }
            catch
            {
                dbCustomer = null;
            }
            return dbCustomer;
        }
    }
}
