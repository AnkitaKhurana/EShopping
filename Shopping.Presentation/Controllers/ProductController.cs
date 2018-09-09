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
        [AllowAnonymous]
        public ActionResult Index()
        {
            ProductsDTO products = ProductLogic.AllProducts();
            return View(products);
        }
    }
}