using Shopping.Data;
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
            try
            {
                List<OrderDTO> orders = new List<OrderDTO>();                
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
                            ProductName = productRow.orderLines.Product.Name

                        });
                    }

                    orders.Add(new OrderDTO()
                    {
                        Id = row.order.Id,
                        OrderStatus = row.order.OrderStatus,
                        CustomerId = row.order.CustomerId,
                        TotalAmount = row.order.TotalAmount,
                        orders = allOrders

                    });
                }

                return orders;

            }

            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Place order for customer 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static OrderDTO Place(Guid customerId)
        {
            try
            {
                decimal sum = 0;
                var cartjoin = (from cartItem in db.CartLines
                                where cartItem.CustomerId == customerId
                                select new { cartItem }).ToList();
                OrderDTO orderDTO = null;
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
                            ProductId = row.cartItem.ProductId
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
                            ProductName = orderLine.Product.Name

                        });

                    }
                    order.TotalAmount = sum;
                    order.OrderStatus = "Placed";
                    db.Orders.Add(order);
                    db.SaveChanges();
                    orderDTO = new OrderDTO()
                    {
                        Id = order.Id,
                        CustomerId = order.CustomerId,
                        TotalAmount = order.TotalAmount,
                        OrderStatus = order.OrderStatus,
                        orders = orders
                    };

                }

                return orderDTO;
            }
            catch
            {
                return null;
            }
        }

    }
}
;