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
        public static CartDTO EditCart(Guid customerId,Guid productId, int quantity)
        {
            try
            {
                CartDTO myCart = CartData.Edit(customerId,productId,quantity);
                return myCart;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
