using Shopping.BLL.Logic;
using Shopping.Presentation.Mapping;
using Shopping.Presentation.ViewModels;
using Shopping.Shared.Constants;
using Shopping.Shared.DTOs;
using System;
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
            if (Session[Constants.SessionConstants.SessionId] == null)
            {
                return HttpNotFound();
            }
            string id = Session[Constants.SessionConstants.SessionId].ToString();
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
            if (Session[Constants.SessionConstants.SessionId] == null)
            {
                return HttpNotFound();
            }
            string id = Session[Constants.SessionConstants.SessionId].ToString();
            CartDTO cart = CartLogic.CustomerCart(new Guid(id));
            Cart cartView = CartMapping.MapCart(cart);
            return View(cartView);
        }

        /// <summary>
        /// Add Product To Cart
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>     

        //public ActionResult AddToCart(Guid? product, string VariantName)
        public ActionResult AddToCart(Guid? product)
        {
            CartDTO cartDTO = new CartDTO();
            if (Request.Form.Count != 0)
            {
                Guid id = new Guid(Request.Form["product"].ToString());
                string VariantName = Request.Form["VariantName"];
                //new Guid(product.ToString());
                CartLogic.AddToCart(new Guid(Session[Constants.SessionConstants.SessionId].ToString()), id, VariantName);

            }
            else
            { 
             Guid id = new Guid(product.ToString());
              CartLogic.AddToCart(new Guid(Session[Constants.SessionConstants.SessionId].ToString()), id, "");

            }

            return RedirectToAction("Cart");
        }


        public ActionResult DeleteFromCart(Guid product)
        {
            CartDTO cartDTO = new CartDTO();
            Guid id = new Guid(product.ToString());
            CartLogic.DeleteFromCart(new Guid(Session[Constants.SessionConstants.SessionId].ToString()), id);
            return RedirectToAction("Cart");
        }


    }
}