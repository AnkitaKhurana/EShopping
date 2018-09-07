using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.DTOs
{
    public class CartLineDTO
    {
        public CartLineDTO()
        {
            this.Cart = new HashSet<CartDTO>();
        }

        public System.Guid Id { get; set; }
        public System.Guid ProductId { get; set; }
        public System.Guid CustomerId { get; set; }
        public System.Guid Quantity { get; set; }

        public virtual ICollection<CartDTO> Cart { get; set; }
        public virtual CustomerDTO Customer { get; set; }
        public virtual ProductDTO Product { get; set; }
    }
}
