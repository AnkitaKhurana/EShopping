using Shopping.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping.DAL.Data;
using Shopping.Shared.Exceptions;

namespace Shopping.BLL.Logic
{
    public class CustomerLogic
    {
        /// <summary>
        /// Register new customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public static bool Register(CustomerDTO customer)
        {
            try
            {
                bool saved = CustomerData.Register(customer);
                return saved;
            }
            catch (EmailAlreadyExists)
            {
                throw new EmailAlreadyExists();
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// FInd Customer 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public static CustomerDTO Find(CustomerDTO customer)
        {
            try
            {
                CustomerDTO foundCustomer = CustomerData.Find(customer);
                return foundCustomer;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static CustomerDTO FindId(Guid? customer)
        {
            try
            {
                CustomerDTO foundCustomer = CustomerData.FindId(customer);
                return foundCustomer;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static CustomerDTO Edit(CustomerDTO customer)
        {
            try
            {
                CustomerDTO c = CustomerData.Edit(customer);
                return c;
            }
            catch 
            {
                return null;
            }
        }
    }
}
