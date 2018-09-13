using Shopping.BLL.Logic;
using Shopping.Shared.Constants;
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
            AllOrdersDTO orderDTO = OrderLogic.Orders(new Guid(Session[Constants.SessionConstants.SessionId].ToString()));
            return View(orderDTO);
        }

        public ActionResult Confirm()
        {
            return RedirectToAction("Edit","Customer",new { id=Session[Constants.SessionConstants.SessionId] });
        }

        // GET: PlaceOrder
        public ActionResult Place()
        {
            OrderDTO orderDTO = OrderLogic.PlaceOrder(new Guid(Session[Constants.SessionConstants.SessionId].ToString()));
            return View(orderDTO);
        }

        public ActionResult Order(Guid order)
        {
            OrderDTO orderDTO = OrderLogic.Find(new Guid(order.ToString()));            
            return View(orderDTO);
        }
    }
}