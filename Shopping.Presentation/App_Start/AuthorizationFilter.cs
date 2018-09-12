using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Shopping.Presentation.App_Start
{
    public class AuthorizationFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// Function to Handle Authentication 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }

            // Check for authorization  
            if (HttpContext.Current.Session["UserName"] == null)
            {
                string ErrorMessage = "";
                if (filterContext.HttpContext.Request.Url.GetComponents(UriComponents.PathAndQuery, UriFormat.SafeUnescaped) == null)
                {
                    ErrorMessage = "";
                }
                else
                {
                    ErrorMessage = "You must be logged in!";
                }

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "controller", "Customer" },
                    { "action", "Login" },
                    { "RedirectResult", filterContext.HttpContext.Request.Url.GetComponents(UriComponents.PathAndQuery, UriFormat.SafeUnescaped) } ,
                    {"Error",ErrorMessage }
                    
                });

                
                //filterContext.Result = new RedirectResult("~/Customer/Login");
            }
        }
    }
}