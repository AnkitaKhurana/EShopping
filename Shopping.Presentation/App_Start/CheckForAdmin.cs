using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shopping.Presentation.App_Start
{
    /// <summary>
    /// Action Filter to handle Admin Check 
    /// </summary>
    public class CheckForAdmin : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var viewBag = filterContext.Controller.ViewBag;
            viewBag.Message = "Welcome " + filterContext.HttpContext.Session["Role"];           
        }
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
           // Nothing to do here
        }
    }
        
}