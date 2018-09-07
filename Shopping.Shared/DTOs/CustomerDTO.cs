using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.DTOs
{
    public class CustomerDTO
    {
        public CustomerDTO()
        {
            this.Cart = new HashSet<CartDTO>();
            this.CartLine = new HashSet<CartLineDTO>();
            this.Order = new HashSet<OrderDTO>();
        }

        public System.Guid Id { get; set; }
        public byte Role { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<CartDTO> Cart { get; set; }
        public virtual ICollection<CartLineDTO> CartLine { get; set; }        
        public virtual ICollection<OrderDTO> Order { get; set; }
    }
}
