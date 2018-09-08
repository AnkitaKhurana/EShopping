using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Presentation.ViewModels
{
    public class CartLine
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }

    }
}
