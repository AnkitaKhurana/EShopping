using System;
using System.Collections.Generic;


namespace Shopping.Shared.DTOs
{

    public class OrderDTO
    {

        public OrderDTO()
        {
            orders = new List<OrderLineDTO>();
            OrderStatus = 1;
        }
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public int? OrderStatus { get; set; }
        public List<OrderLineDTO> orders;
        public DateTime? OrderDate { get; set; }
    }
}
