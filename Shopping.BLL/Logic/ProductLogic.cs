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
                ProductsDTO saved =  ProductData.AllProducts(null,null);
                return saved;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public static ProductsDTO AllProductsInCategory(Guid categoryId)
        {
            try
            {
                ProductsDTO saved = ProductData.AllProducts(null, categoryId);
                return saved;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public static ProductsDTO AllProductsInSearch(string searchString)
        {
            try
            {
                ProductsDTO saved = ProductData.AllProducts( searchString,null);
                return saved;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public static ProductsDTO AllProductsInSearchAndCategory(string searchString, Guid categoryId )
        {
            try
            {
                ProductsDTO saved = ProductData.AllProducts(searchString, categoryId);
                return saved;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
