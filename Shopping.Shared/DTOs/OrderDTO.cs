using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.DTOs
{
    public class OrderDTO
    {
        public OrderDTO()
        {
            this.OrderLine = new HashSet<OrderLineDTO>();
        }

        public System.Guid Id { get; set; }
        public System.Guid CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; }

        public virtual CustomerDTO Customer { get; set; }
        public virtual ICollection<OrderLineDTO> OrderLine { get; set; }
    }
}
