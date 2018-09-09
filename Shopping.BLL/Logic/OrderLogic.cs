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
        public static AllOrdersDTO Orders(Guid CustomerId)
        {
            try
            {
                AllOrdersDTO saved = new AllOrdersDTO();
                saved.Orders = OrderData.Orders(CustomerId);               
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
                // Add asyn update topProducts                
                TopProductsData.UpdateTopProducts(saved);
                return saved;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
