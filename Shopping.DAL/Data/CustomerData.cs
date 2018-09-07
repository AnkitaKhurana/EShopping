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
        private static ShoppingMDFEntities db = new ShoppingMDFEntities();


        public static bool Register(CustomerDTO customer)
        {
            try
            {
                Customer dbCustomer = CustomerMapping.MapCustomer(customer);
                db.Customer.Add(dbCustomer);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;

        }
               
    }
}
