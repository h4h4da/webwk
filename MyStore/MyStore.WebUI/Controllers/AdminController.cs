using MyStore.Domain.Abstract;
using MyStore.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyStore.WebUI.Controllers
{
  
    public class AdminController : Controller
    {
        private IProductsRepository repository;
        public AdminController(IProductsRepository productRepository)
        {
            this.repository = productRepository;
        }

        public ActionResult Index()
        {
            return View(repository.Products.ToList());
        }

        public ActionResult Edit(int id)
        {
            Product product = repository.Products.FirstOrDefault(p => p.Id == id);
            IEnumerable<SelectListItem> selectListItem = 
                repository.Categories.ToList().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            ViewBag.CategoryList = selectListItem;
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["msg"] = string.Format("{0} 保存成功", product.Name);
                return RedirectToAction("Index");
            }
            else {
                
                IEnumerable<SelectListItem> selectListItem = repository.Categories.ToList().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                ViewBag.CategoryList = selectListItem;
                return View(product);
            }
        }


    }
}