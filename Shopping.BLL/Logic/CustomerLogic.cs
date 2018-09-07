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
        public static bool Register(CustomerDTO customer)
        {
            try
            {
                var saved = CustomerData.Register(customer);
                return saved;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
