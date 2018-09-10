using Shopping.BLL.Logic;
using Shopping.Presentation.CustomModelBinders;
using Shopping.Shared.DTOs;
using Shopping.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
            try
            {
                if (ModelState.IsValid)
                {

                    CustomerDTO foundCustomer = CustomerLogic.Find(user);
                    if (foundCustomer != null)
                    {
                        Session["UserName"] = foundCustomer.Name;
                        Session["Email"] = foundCustomer.Email;
                        Session["Role"] = (foundCustomer.Role == 1 ? "Admin" : "Normal Customer");
                        Session["Id"] = foundCustomer.Id;
                        Session["Address1"] = foundCustomer.Address1;
                        Session["Address2"] = foundCustomer.Address2;
                        Session["Address3"] = foundCustomer.Address3;

                    }

                    return RedirectToAction("Details");
                }
                return View(user);
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                return View("Error");
            }
            catch (Exception)
            {
                return View("Error");
            }

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
            try
            {
                if (ModelState.IsValid)
                {
                    CustomerLogic.Register(customer);
                    return RedirectToAction("Login");
                }
                return View(customer);
            }
            catch (EmailAlreadyExists error)
            {
                ViewBag.ErrorMessage = error.Message;
                return View("Error");
            }
        }

        // GET: Customer/Details/
        public ActionResult Details()
        {
            if (Session["id"] == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Response.Cookies.Clear();
            return View("Login");
        }



        public ActionResult Edit(Guid? id)
        {
            CustomerDTO customer = new CustomerDTO();            
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                customer = CustomerLogic.FindId(id);
                if (customer == null)
                {
                    return HttpNotFound();
                }
            }
            catch
            {
                return View(customer);
            }
            
            return View(customer);

        }

     
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Address1")] CustomerDTO customer)
        {
            customer.Id = new Guid(Session["id"].ToString());
            if (ModelState.IsValid)
            {
                CustomerLogic.Edit(customer);
                return RedirectToAction("Place", "Order");
            }
            return RedirectToAction("Details");
        }

       
    }
}