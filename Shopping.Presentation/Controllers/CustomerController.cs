using Shopping.BLL.Logic;
using Shopping.Presentation.CustomModelBinders;
using Shopping.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Shopping.Presentation.Controllers
{
    public class CustomerController : Controller
    {

        // GET: Customers
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        // Login: Customer
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]

        public ActionResult Login([Bind(Include = "Id,Email,Password")] CustomerDTO user)
        {
            if (ModelState.IsValid)
            {
               
                CustomerDTO foundCustomer = CustomerLogic.Find(user);
                if(foundCustomer != null)
                {
                    Session["UserName"] = foundCustomer.Name;
                    Session["Email"] = foundCustomer.Email;
                    Session["Role"] = (foundCustomer.Role == 1 ? "Admin" : "Normal Customer");
                    Session["Id"] = foundCustomer.Id;
                    Session["Address1"] = foundCustomer.Address1;
                    Session["Address2"] = foundCustomer.Address2;
                    Session["Address3"] = foundCustomer.Address3;             

                }

                return RedirectToAction("Index");
            }
            return View(user);
        }

        // Register: Customer
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]

        public ActionResult Register([ModelBinder(typeof(AddCustomerRole))] CustomerDTO customer)
        {
            if (ModelState.IsValid)
            {
                CustomerLogic.Register(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }

       // GET: Users/Details/
        public ActionResult Details()
        {           
            if (Session["id"] == null)
            {
                return HttpNotFound();
            }           
            return View();
        }



    }
}