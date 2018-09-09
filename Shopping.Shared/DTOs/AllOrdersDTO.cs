using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.DTOs
{
    public class AllOrdersDTO
    {
        public AllOrdersDTO()
        {
            this.Orders = new List<OrderDTO>();
        }
        public List<OrderDTO> Orders;
    }
}
