using Shopping.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shopping.DAL.Data;

namespace Shopping.BLL.Logic
{
    public class CustomerLogic
    {
        public static bool Register (CustomerDTO customer)
        {
            try
            {
                bool saved = CustomerData.Register(customer);
                return saved;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public static CustomerDTO Find (CustomerDTO customer)
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
    }
}
