using Shopping.Presentation.App_Start;
using Shopping.Shared.DTOs;
using Shopping.BLL.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shopping.Presentation.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            HomePageDTO td = TopProductsLogic.HomePageProducts();
            return View(td);
        }
               

        /// <summary>
        /// Admin page Check 
        /// </summary>
        /// <returns></returns>
        [CheckForAdmin]
        public ActionResult Admin()
        {
            ViewBag.Message = "Who are you?";

            return View();
        }
    }
}