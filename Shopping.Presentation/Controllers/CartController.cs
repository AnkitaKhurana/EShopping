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
        // GET: Cart
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cart(CartItem cart) //(Guid customerId, Guid productId, int quantity)//[Bind(Include = "Product,Id,Quantity")]
        {
            if (ModelState.IsValid)
            {
                CartDTO cartDTO = new CartDTO();
                CartLogic.AddToCart(new Guid(Session["id"].ToString()), new Guid("34a7fc5a-deb7-40eb-863d-1ced2d8dc5a8"));
                //CartLogic.EditCart(new Guid(Session["id"].ToString()), cart.Product.Id, cart.Quantity);
                //CartLogic.EditCart(customerId, productId, quantity);      
                return RedirectToAction("Index");
            }
            return View();
        }

        
        public ActionResult AddToCart(Guid? product)
        {
            CartDTO cartDTO = new CartDTO();
            Guid id = new Guid(product.ToString());
            CartLogic.AddToCart(new Guid(Session["id"].ToString()), id);

            return RedirectToAction("Index");
        }


    }
}