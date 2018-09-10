using Shopping.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shopping.Presentation.CustomModelBinders
{
    public class AddCustomerRole : IModelBinder
    {
        /// <summary>
        /// Update Customer to be of Normal Type 
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="bindingContext"></param>
        /// <returns></returns>
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;
            return new CustomerDTO()
            {
                Id = Guid.NewGuid(),
                Name = request.Form.Get("Name"),
                Email = request.Form.Get("Email"),
                Password = request.Form.Get("Password"),
                Address1 = request.Form.Get("Address1"),
                Address2 = request.Form.Get("Address2"),
                Address3 = request.Form.Get("Address3"),
                Role = 0
            };
        }
    }
}