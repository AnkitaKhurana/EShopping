using Shopping.DAL.Data;
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
        public static HomePageDTO HomePageProducts()
        {
            try
            {
                HomePageDTO homePageDTO = new HomePageDTO();
                var OrderedList = TopProductsData.GetTopProducts().Products.OrderBy(x => x.Category.TotalSaleQuantity).ThenBy(y=>y.TotalQuantitySale).ToList();
                var count = OrderedList.Count();
              
                for(int j = 0; j < 6; j++)
                {
                    TrendingDTO category = new TrendingDTO()
                    {
                        Category = OrderedList[j*(5)].Category,
                        Rank = j+1
                    };

                    ProductsDTO pt = new ProductsDTO();
                    for (int i = 0; i < 5; i++)
                    {
                        pt.Products.Add(OrderedList[(j * 5) + i]);
                    }
                    homePageDTO.Trending.Add(category);
                   
                }
               
                return homePageDTO;
            }
            catch (Exception  e)
            {
                return null;
            }
        }
    }
}
