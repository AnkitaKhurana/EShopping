using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Presentation.ViewModels

{
    public class CartItem
    {
        public Guid? Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
