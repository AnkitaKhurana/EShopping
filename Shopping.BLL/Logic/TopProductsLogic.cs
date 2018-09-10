using Shopping.DAL.Data;
using Shopping.Shared.Constants;
using Shopping.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.BLL.Logic
{
    public class TopProductsLogic
    {

        /// <summary>
        /// Return Main Page DTO from analytics to get trending products
        /// </summary>
        /// <returns></returns>
        public static HomePageDTO HomePageProducts()
        {
            try
            {
                HomePageDTO homePageDTO = new HomePageDTO();
                var OrderedList = TopProductsData.GetTopProducts().Products.OrderBy(x => x.Category.Id).ThenByDescending(cat => cat.TotalQuantitySale).ToList();

                for (int j = 0; j < Constants.TotalCategories; j++)
                {
                    TrendingDTO category = new TrendingDTO()
                    {
                        Category = OrderedList[j * (5)].Category,
                        Rank = j + 1
                    };

                    List<ProductDTO> temp = new List<ProductDTO>();
                    for (int i = 0; i < Constants.ProductsToDisplay; i++)
                    {
                        temp.Add(OrderedList[(j * 5) + i]);
                    }
                    var sortedProducts = temp.OrderByDescending(x => x.TotalQuantitySale).ToList();
                    ProductsDTO pt = new ProductsDTO();
                    for (int i = 0; i < Constants.ProductsToDisplay; i++)
                    {
                        pt.Products.Add(sortedProducts[i]);
                    }
                    category.TopProducts.Add(pt);
                    homePageDTO.Trending.Add(category);
                }
                return homePageDTO;
            }
            catch
            {
                return null;
            }
        }
    }
}
