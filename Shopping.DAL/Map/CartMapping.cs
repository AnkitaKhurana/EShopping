using Shopping.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBCart = Shopping.Data;

namespace Shopping.DAL.Map
{
    public class CartMapping
    {

        public static CartDTO MapCart(IEnumerable<CartDTO> cart)
        {
            CartDTO result = null;
            foreach (var item in cart)
            {
                result = new CartDTO
                {
                    Id = item.Id
                };
            }

            return result;
        }

    }
}
