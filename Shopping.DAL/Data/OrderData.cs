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
                    Order order = new Order() { Id = Guid.NewGuid()};
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
            catch (Exception e)
            {
                return null;
            }
        }

    }
}
;