using Shopping.DAL.Data;
using Shopping.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.BLL.Logic
{
    public class CartLogic
    {
        /// <summary>
        /// Return Customer Cart 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CartDTO CustomerCart(Guid id)
        {
            try
            {
                CartDTO myCart = CartData.Cart(id);
                return myCart;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Add Product to Customer Cart 
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static CartDTO AddToCart(Guid customerId, Guid productId)
        {
            try
            {
                CartDTO myCart = CartData.AddToCart(customerId, productId);
                return myCart;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
