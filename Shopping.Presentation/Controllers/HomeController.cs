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

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [CheckForAdmin]
        public ActionResult Admin()
        {
            ViewBag.Message = "Who are you?";

            return View();
        }
    }
}