using Shopping.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Presentation.ViewModels
{
    public class Product
    {
        public Product()
        {
            this.Variants = new List<string>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public int TotalQuantitySale { get; set; }
        public CategoryDTO Category { get; set; }
        public List<string> Variants { get; set; }
    }
}
