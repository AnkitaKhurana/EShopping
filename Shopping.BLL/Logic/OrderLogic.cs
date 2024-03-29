﻿using Shopping.DAL.Data;
using Shopping.Shared.DTOs;
using System;

namespace Shopping.BLL.Logic
{
    public class OrderLogic
    {
        /// <summary>
        /// Get all orders of customer
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public static AllOrdersDTO Orders(Guid CustomerId)
        {
            try
            {
                AllOrdersDTO saved = new AllOrdersDTO()
                {
                    Orders = OrderData.Orders(CustomerId)
                };
                return saved;
            }
            catch (Exception)
            {
                return null;
            }

        }

        /// <summary>
        /// Place Order 
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public static OrderDTO PlaceOrder(Guid CustomerId)
        {
            try
            {
                OrderDTO saved = OrderData.Place(CustomerId);
                TopProductsData.UpdateTopProducts(saved);
                return saved;
            }
            catch (Exception)
            {
                return null;
            }

        }

        /// <summary>
        /// Find Order By Order ID
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static OrderDTO Find(Guid orderId)
        {
            try
            {
                OrderDTO saved = OrderData.FindOrder(orderId);           
                return saved;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
