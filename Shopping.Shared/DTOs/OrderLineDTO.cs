using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.DTOs
{
    public class OrderLineDTO
    {
        public System.Guid Id { get; set; }
        public System.Guid OrderId { get; set; }
        public System.Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual OrderDTO Order { get; set; }
        public virtual ProductDTO Product { get; set; }
    }
}
