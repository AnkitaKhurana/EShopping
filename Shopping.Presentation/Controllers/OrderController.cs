using Shopping.BLL.Logic;
using Shopping.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shopping.Presentation.Controllers
{
    public class OrderController : Controller
    {
        // GET: Orders
        public ActionResult Index()
        {
            return View();
        }

        // GET: PlaceOrder
        public ActionResult Place()
        {
            OrderDTO orderDTO = OrderLogic.PlaceOrder(new Guid(Session["id"].ToString()));
            return View(orderDTO);
        }
    }
}