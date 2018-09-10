using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Presentation.ViewModels
{
    public class Cart
    {
        public Cart()
        {
            Id = Guid.NewGuid();
            items = new List<CartItem>();
        }
        public Guid? Id { get; set; }
        public List<CartItem> items { get; set; }
    }
}
