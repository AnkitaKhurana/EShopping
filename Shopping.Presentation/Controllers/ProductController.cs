using Shopping.Shared.DTOs;
using Shopping.BLL.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shopping.Presentation.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        //[allowanonymous]
        //public actionresult index()
        //{
        //    productsdto products = productlogic.allproducts();
        //    return view(products);
        //}

        [AllowAnonymous]
        public ActionResult Index(string searchString)
        {
            ProductsDTO products;
            if (String.IsNullOrEmpty(searchString))
            {
                products = ProductLogic.AllProducts();
            }
            else
            {
                products = ProductLogic.AllProductsInSearch(searchString);
            }
            
            return View(products);

        }
    }
}