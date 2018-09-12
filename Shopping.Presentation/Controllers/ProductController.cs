using Shopping.Shared.DTOs;
using Shopping.BLL.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopping.Presentation.ViewModels;

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
        public ActionResult Index()
        {
            ProductsDTO  products = ProductLogic.AllProducts();
            return View(products);

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
        public ActionResult SearchResults(string searchString)
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
            return View(homePageDTO);
            //ProductsDTO products = null;
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    products = ProductLogic.AllProductsInSearch(searchString);
            //}
            //return View(products);
        }
    }
}