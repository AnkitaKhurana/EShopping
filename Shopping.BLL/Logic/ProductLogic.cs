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

        /// <summary>
        /// Return All products 
        /// </summary>
        /// <returns></returns>
        public static ProductsDTO AllProducts()
        {
            try
            {
                ProductsDTO saved = ProductData.AllProducts(null, null);
                return saved;
            }
            catch (Exception)
            {
                return null;
            }

        }

        /// <summary>
        /// Return All product in Catgeory 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Return Product in Search Box
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public static ProductsDTO AllProductsInSearch(string searchString)
        {
            try
            {
                ProductsDTO saved = ProductData.AllProducts(searchString, null);
                return saved;
            }
            catch (Exception)
            {
                return null;
            }

        }

        /// <summary>
        /// Return products in Category and Search 
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public static ProductsDTO AllProductsInSearchAndCategory(string searchString, Guid categoryId)
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

        /// <summary>
        /// Get one product 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static ProductDTO Product(Guid productID)
        {
            try
            {
                ProductDTO saved = ProductData.ProductDetail(productID);
                return saved;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
