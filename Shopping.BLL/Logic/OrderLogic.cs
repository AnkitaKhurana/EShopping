using Shopping.DAL.Data;
using Shopping.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.BLL.Logic
{
    public class OrderLogic
    {
        public static OrderDTO PlaceOrder(Guid CustomerId)
        {
            try
            {
                OrderDTO saved = OrderData.Place(CustomerId);
                return saved;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
