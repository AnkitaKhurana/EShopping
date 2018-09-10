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
        public ActionResult Index(string searchString)
        {
            string category = null;
            Guid? categoryId = null;
            if (Request.Form["categories.categories"] != null)
            {
                category = Request.Form["categories.categories"].ToString();
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

            ProductFilters productFilters = new ProductFilters
            {
                productsDTOs = products,
                categories = CategoryLogic.AllCategories()
            };

            return View(productFilters);
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
    }
}