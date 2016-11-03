using MyStore.Domain.Abstract;
using MyStore.Domain.Concrete;
using MyStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyStore.WebUI.Controllers
{
    public class CustomerController : Controller
    {
        private IProductsRepository repository;

        public CustomerController(IProductsRepository productReposity)
        {
            this.repository = productReposity;
        }
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Customer customerEntry = repository.Customers.FirstOrDefault(c => c.UserName == model.UserName && c.PassWord == model.PassWord);
                if (customerEntry != null)
                {
                    HttpContext.Session["Customer"] = customerEntry;
                    return Redirect(returnUrl ?? Url.Action("List", "Product"));
                }
                else {
                    ModelState.AddModelError("","用户名或密码不正确");
                    return View();
                }
            }
            else 
                return View();
        }

        public PartialViewResult Summary(Customer customer)
        {
            return PartialView(customer);
        }
    }
}