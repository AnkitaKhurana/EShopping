using Shopping.DAL.Data;
using Shopping.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.BLL.Logic
{
    public class ProductLogic
    {
        public static ProductsDTO AllProducts()
        {
            try
            {
                ProductsDTO saved =  ProductData.AllProducts();
                return saved;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
