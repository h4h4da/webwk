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

        public CartController(IProductsRepository repositoryParam)
        {
            this.repository = repositoryParam;
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

        public RedirectToRouteResult AddToCart(Cart cart,int id, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.Id == id);
            if (product != null) cart.AddItem(product,1);
            return RedirectToAction("Index", new { returnUrl });
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
    }
}