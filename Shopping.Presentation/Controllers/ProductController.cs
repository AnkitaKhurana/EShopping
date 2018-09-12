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
        public ActionResult Index(int? page=1)
        {
            ProductsDTO  products = ProductLogic.AllProducts();            
            int pageSize = Constants.PageSize;
            int pageNumber = (page ?? 1);
            return View(products.Products.ToPagedList(pageNumber, pageSize));            
        }




        //public ActionResult Index(string searchString)
        //{
        //    string category = null;
        //    Guid? categoryId = null;
        //    if (Request.Form["categories.categories"] != null)
        //    {
        //        category = Request.Form["categories.categories"].ToString();
        //        if (!String.IsNullOrEmpty(category))
        //        {
        //            categoryId = new Guid(category);
        //        }
        //    }

        //    ProductsDTO products;
        //    if (String.IsNullOrEmpty(searchString))
        //    {
        //        if (!categoryId.HasValue)
        //            products = ProductLogic.AllProducts();
        //        else
        //            products = ProductLogic.AllProductsInCategory(new Guid(categoryId.ToString()));
        //    }
        //    else
        //    {
        //        if (!categoryId.HasValue)
        //            products = ProductLogic.AllProductsInSearch(searchString);
        //        else
        //            products = ProductLogic.AllProductsInSearchAndCategory(searchString, new Guid(categoryId.ToString()));
        //    }

        //    ProductFilter productFilters = new ProductFilter
        //    {
        //        productsDTOs = products,
        //        categories = CategoryLogic.AllCategories()
        //    };
        //    HomePageDTO homePageDTO = new HomePageDTO()
        //    {
        //        productFilter = productFilters,
        //        Trending = null
        //    };
        //    return View(homePageDTO);

        //}




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
        public ActionResult SearchResults(string searchString, int? page = 1)
        {
            string category = null;
            Guid? categoryId = null;
            if (Request.Form["category"] != null)
            {
                category = Request.Form["category"].ToString();
                if (!String.IsNullOrEmpty(category))
                {
                    categoryId = new Guid(category);
                }
            }

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
            int pageSize = Constants.PageSize;
            int pageNumber = (page ?? 1);
            return View(homePageDTO.productFilter.productsDTOs.Products.ToPagedList(pageNumber, pageSize));
            return View(homePageDTO);
           
        }
    }
}