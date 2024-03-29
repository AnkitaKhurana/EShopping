﻿using Shopping.Data;
using Shopping.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.DAL.Data
{
    public class OrderData
    {
        private static ShoppingDatabaseEntities db = new ShoppingDatabaseEntities();

        /// <summary>
        /// Get All orders of customer 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static List<OrderDTO> Orders(Guid customerId)
        {
            List<OrderDTO> orders = new List<OrderDTO>();

            try
            {
                var customerOrders = (from order in db.Orders
                                      where order.CustomerId == customerId
                                      select new { order }).ToList();
                foreach (var row in customerOrders)
                {
                    var products = (from orderLines in db.OrderLines
                                    where orderLines.OrderId == row.order.Id
                                    select new { orderLines }).ToList();
                    List<OrderLineDTO> allOrders = new List<OrderLineDTO>();
                    foreach (var productRow in products)
                    {
                        allOrders.Add(new OrderLineDTO()
                        {
                            Id = productRow.orderLines.Id,
                            OrderId = productRow.orderLines.OrderId,
                            ProductId = productRow.orderLines.ProductId,
                            Quantity = productRow.orderLines.Quantity,
                            ProductName = productRow.orderLines.Product.Name,
                            VariantName = productRow.orderLines.VariantName

                        });
                    }

                    orders.Add(new OrderDTO()
                    {
                        Id = row.order.Id,
                        OrderStatus = row.order.OrderStatus,
                        CustomerId = row.order.CustomerId,
                        TotalAmount = row.order.TotalAmount,
                        orders = allOrders,
                        OrderDate = row.order.OrderDate                        

                    });
                }

            }
            catch
            {
                orders = null;
            }
            return orders;
        }

        /// <summary>
        /// Place order for customer 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static OrderDTO Place(Guid customerId)
        {
            OrderDTO orderDTO = null;
            try
            {
                decimal sum = 0;
                var cartjoin = (from cartItem in db.CartLines
                                where cartItem.CustomerId == customerId
                                select new { cartItem }).ToList();
                if (cartjoin.Count() != 0)
                {
                    Order order = new Order() { Id = Guid.NewGuid() };
                    order.CustomerId = customerId;

                    List<OrderLineDTO> orders = new List<OrderLineDTO>();
                    foreach (var row in cartjoin)
                    {
                        OrderLine orderLine = new OrderLine()
                        {
                            Id = Guid.NewGuid(),
                            Quantity = row.cartItem.Quantity,
                            ProductId = row.cartItem.ProductId,
                            VariantName = row.cartItem.VariantName
                        };
                        db.OrderLines.Add(orderLine);
                        order.OrderLines.Add(orderLine);
                        sum += row.cartItem.Product.Price * row.cartItem.Quantity;
                        db.CartLines.Remove(row.cartItem);

                        orders.Add(new OrderLineDTO()
                        {
                            Id = orderLine.Id,
                            OrderId = orderLine.OrderId,
                            ProductId = orderLine.ProductId,
                            Quantity = orderLine.Quantity,
                            ProductName = orderLine.Product.Name,
                            VariantName = orderLine.VariantName

                        });

                    }
                    order.TotalAmount = sum;
                    order.OrderStatus = 1;
                    order.OrderDate = DateTime.Now;
                    db.Orders.Add(order);
                   
                    db.SaveChanges();
                    orderDTO = new OrderDTO()
                    {
                        Id = order.Id,
                        CustomerId = order.CustomerId,
                        TotalAmount = order.TotalAmount,
                        OrderStatus = order.OrderStatus,
                        orders = orders,
                        OrderDate = order.OrderDate
                    };

                }

            }
            catch
            {
                orderDTO = null;
            }
            return orderDTO;
        }

        /// <summary>
        /// Find Order of order Id 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static OrderDTO FindOrder(Guid orderId)
        {
            OrderDTO order = null;
            try
            {
                List<OrderLineDTO> orders = new List<OrderLineDTO>();
                var orderQuery = (from line in db.OrderLines
                                  where line.OrderId == orderId
                                  select line).ToList();

                foreach (var orderLine in orderQuery)
                {
                    orders.Add(new OrderLineDTO()
                    {
                        Id = orderLine.Id,
                        OrderId = orderLine.OrderId,
                        ProductId = orderLine.ProductId,
                        Quantity = orderLine.Quantity,
                        ProductName = orderLine.Product.Name,
                        VariantName = orderLine.VariantName
                    });
                }

                var orderInDb = (from line in db.Orders
                                 where line.Id == orderId
                                 select line).FirstOrDefault();

                order = new OrderDTO()
                {
                    Id = orderInDb.Id,
                    orders = orders,
                    CustomerId = orderInDb.CustomerId,
                    OrderStatus = orderInDb.OrderStatus,
                    TotalAmount = orderInDb.TotalAmount,
                    OrderDate = orderInDb.OrderDate 
                };
            }
            catch
            {
                order = null;
            }
            return order;
        }

    }
}
;