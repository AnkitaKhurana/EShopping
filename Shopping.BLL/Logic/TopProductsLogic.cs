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
                var OrderedList = TopProductsData.GetTopProducts().Products.OrderBy(x => x.Category.Id).ThenByDescending(cat => cat.TotalQuantitySale).ToList();
                  
                for(int j = 0; j < 6; j++)
                {
                    TrendingDTO category = new TrendingDTO()
                    {
                        Category = OrderedList[j*(5)].Category,
                        Rank = j+1
                    };

                    List<ProductDTO> temp = new List<ProductDTO>();
                    for (int i = 0; i < 5; i++)
                    {
                        temp.Add(OrderedList[(j * 5) + i]);
                    }
                    var sortedProducts = temp.OrderByDescending(x => x.TotalQuantitySale).ToList();
                    ProductsDTO pt = new ProductsDTO();
                    for (int i = 0; i < 5; i++)
                    {
                        pt.Products.Add(sortedProducts[i]);//(OrderedList[(j * 5) + i]);
                    }
                    category.TopProducts.Add(pt);
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
