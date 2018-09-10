using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.DTOs
{
    public class CartDTO
    {

        public CartDTO()
        {
            Id = Guid.NewGuid();
            items = new List<CartItemDTO>();
        }
        public Guid? Id { get; set; }
        public List<CartItemDTO> items;
    }
}
