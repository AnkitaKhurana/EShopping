using Shopping.Shared.DTOs;
using Shopping.BLL.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopping.Presentation.ViewModels;
using PagedList;
using Shopping.Shared.Constants;

namespace Shopping.Presentation.Controllers
{
    public class ProductController : Controller
    {

        /// <summary>
        /// Get All Products
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index(int? page = 1)
        {
            ProductsDTO products = ProductLogic.AllProducts();
            int pageSize = Constants.PagingConstants.PageSize;
            int pageNumber = (page ?? 1);
            return View(products.Products.ToPagedList(pageNumber, pageSize));
        }


        /// <summary>
        /// Get Product Details
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public ActionResult Details(Guid? product)
        {

            if (product.HasValue)
            {
                Guid id = new Guid(product.ToString());
                ProductDTO p = ProductLogic.Product(id);
                return View(p);
            }
            return View();

        }

        [AllowAnonymous]
        public ActionResult SearchResults(string searchString, string category, int? page = 1)
        {

            Guid? categoryId = null;

            if (!String.IsNullOrEmpty(category))
            {
                categoryId = new Guid(category);
            }

            ViewBag.category = category;
            ViewBag.searchString = searchString;

            ProductsDTO products;
            if (String.IsNullOrEmpty(searchString))
            {
                if (!categoryId.HasValue)
                    products = ProductLogic.AllProducts();
                else
                    products = ProductLogic.AllProductsInCategory(new Guid(categoryId.ToString()));
            }
            else
            {
                if (!categoryId.HasValue)
                    products = ProductLogic.AllProductsInSearch(searchString);
                else
                    products = ProductLogic.AllProductsInSearchAndCategory(searchString, new Guid(categoryId.ToString()));
            }

            ProductFilter productFilters = new ProductFilter
            {
                productsDTOs = products,
                categories = CategoryLogic.AllCategories()
            };
            HomePageDTO homePageDTO = new HomePageDTO()
            {
                productFilter = productFilters,
                Trending = null
            };
            int pageSize = Constants.PagingConstants.PageSize;
            int pageNumber = (page ?? 1);
            return View(homePageDTO.productFilter.productsDTOs.Products.ToPagedList(pageNumber, pageSize));

        }
    }
}