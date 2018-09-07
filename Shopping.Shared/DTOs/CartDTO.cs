using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.DTOs
{
    public class CartDTO
    {
        public System.Guid Id { get; set; }
        public System.Guid CartStatusId { get; set; }
        public System.Guid CustomerId { get; set; }
        public decimal TotalAmount { get; set; }

        public virtual CartLineDTO CartLine { get; set; }
        public virtual CustomerDTO Customer { get; set; }
    }
}
