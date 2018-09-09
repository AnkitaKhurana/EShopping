using System;
using System.Collections.Generic;

namespace Shopping.Shared.DTOs
{   
    
    public class OrderLineDTO
    {
        public Guid? Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }    
        public string ProductName { get; set; }
    }
}
