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
        public static CartDTO MyCart(Guid? id)
        {
            try
            {
                CartDTO myCart = CustomerData.MyCart(id);
                return myCart;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
