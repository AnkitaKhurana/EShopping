
using Shopping.Presentation.ViewModels;
using Shopping.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shopping.Presentation.Mapping
{
    public class CartMapping
    {
        /// <summary>
        /// Map Cart to DTO
        /// </summary>
        /// <param name="cartDTO"></param>
        /// <returns></returns>
        public static Cart MapCart(CartDTO cartDTO)
        {
            List<CartItem> items = new List<CartItem>();

            foreach (var item in cartDTO.items)
            {
                var productItem = item.Product;

                Product product = new Product()
                {
                    Name = productItem.Name,
                    Description = productItem.Description,
                    ImageURL = productItem.ImageURL,
                    Price = productItem.Price,
                    TotalQuantitySale = productItem.TotalQuantitySale,
                    Category = productItem.Category,
                    Variants = new List<string>(productItem.Variants)
                };
                items.Add(new CartItem()
                {
                    Quantity = item.Quantity,
                    Product = product,
                    Id = item.Id
                });
            }

            Cart viewCart = new Cart()
            {
                Id = cartDTO.Id,
                items = items
            };

            return viewCart;
        }
    }
}