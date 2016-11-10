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
    public class CartController : Controller
    {
        // GET: Cart

        private IProductsRepository repository;
        private IOrderProcessor orderProcessor;
        public CartController(IProductsRepository repositoryParam,IOrderProcessor proc)
        {
            this.repository = repositoryParam;
            this.orderProcessor = proc;
        }

        private Cart GetCart()
        {

            Cart cart = Session["Cart"] as Cart;
            if (cart == null)
            {
                Session["Cart"] = cart;
            }
            return cart;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel{
                Cart=GetCart(),
                ReturnUrl=returnUrl
            });

        }

        public int AddToCart(Cart cart,int id, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) cart.AddItem(product,1);
            return 23;
        }
        public RedirectToRouteResult RemoveFromCart(Cart cart, int id, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("index",new { returnUrl});
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingAddress());
        }
        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingAddress shippingAddress,Customer customer) 
        {
            if (cart.Line.Count() == 0) {
                ModelState.AddModelError("", "抱歉，购物车是空的，无法结算！");

            }
            if (customer.Id == 0)
            {
                ModelState.AddModelError("", "抱歉，请先登录！");
            }
            if (ModelState.IsValid)
            {
               
                orderProcessor.ProcessOrder(cart, shippingAddress,customer);
                cart.Clear();
                return View("Complete");
            }
            else
            {
                return View(new ShippingAddress());
            }
        }
    }
}