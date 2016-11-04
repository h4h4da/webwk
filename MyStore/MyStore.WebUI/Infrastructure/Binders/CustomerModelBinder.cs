using MyStore.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MyStore.WebUI.Infrastructure.Binders
{
    public class CustomerModelBinder:IModelBinder
    {

        private const string sessionKey = "Customer";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Customer customer = null;
            if (controllerContext.HttpContext.Session != null)
            {
                customer = controllerContext.HttpContext.Session[sessionKey] as Customer; 
            }
            if (customer == null)
            {
                customer = new Customer();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = customer;
                }
            }
            return customer;

        }
    }
}