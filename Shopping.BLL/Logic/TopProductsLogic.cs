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
                for (int categoryIndex = 0; categoryIndex < Constants.TotalCategories; categoryIndex++)
                {
                    TrendingDTO category = new TrendingDTO()
                    {
                        Category = OrderedList[categoryIndex * (5)].Category,
                        Rank = categoryIndex + 1
                    };

                    List<ProductDTO> temp = new List<ProductDTO>();
                    for (int product = 0; product < Constants.ProductsToDisplay; product++)
                    {
                        temp.Add(OrderedList[(categoryIndex * 5) + product]);
                    }
                    var sortedProducts = temp.OrderByDescending(x => x.TotalQuantitySale).ToList();
                    ProductsDTO pt = new ProductsDTO();
                    for (int product = 0; product < Constants.ProductsToDisplay; product++)
                    {
                        pt.Products.Add(sortedProducts[product]);
                    }
                    category.TopProducts.Add(pt);
                    homePageDTO.Trending.Add(category);
                    homePageDTO.productFilter = new ProductFilter
                    {
                        productsDTOs = ProductLogic.AllProducts(),
                        categories = CategoryLogic.AllCategories()
                    };
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
