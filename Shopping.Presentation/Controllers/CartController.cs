using Shopping.BLL.Logic;
using Shopping.Presentation.Mapping;
using Shopping.Presentation.ViewModels;
using Shopping.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shopping.Presentation.Controllers
{
    public class CartController : Controller
    {
        /// <summary>
        /// Cart Summary Funcytion 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Session["id"] == null)
            {
                return HttpNotFound();
            }
            string id = Session["id"].ToString();
            CartDTO cart = CartLogic.CustomerCart(new Guid(id));
            Cart cartView = CartMapping.MapCart(cart);
            return View(cartView);
        }

        /// <summary>
        /// Cart Summary Function 
        /// </summary>
        /// <returns></returns>
        public ActionResult Cart()
        {
            if (Session["id"] == null)
            {
                return HttpNotFound();
            }
            string id = Session["id"].ToString();
            CartDTO cart = CartLogic.CustomerCart(new Guid(id));
            Cart cartView = CartMapping.MapCart(cart);
            return View(cartView);
        }

        /// <summary>
        /// Add Product To Cart
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>     
        
        public ActionResult AddToCart(Guid product)
        {
            CartDTO cartDTO = new CartDTO();
            Guid id = new Guid(product.ToString());
            CartLogic.AddToCart(new Guid(Session["id"].ToString()), id);
            return RedirectToAction("Cart");
        }


    }
}