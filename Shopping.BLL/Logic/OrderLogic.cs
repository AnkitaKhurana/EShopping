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
        public static List<OrderDTO> Orders(Guid CustomerId)
        {
            try
            {
                List<OrderDTO> saved = OrderData.Orders(CustomerId);
                return saved;
            }
            catch (Exception)
            {
                return null;
            }

        }
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
