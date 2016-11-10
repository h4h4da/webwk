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
        public ActionResult CategoryIndex()
        {
            return View(repository.Categories.ToList());
        }

        private IEnumerable<SelectListItem> GetCategorySelectList()
        {
            return
                repository.Categories.ToList().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
        }
        public ActionResult Edit(int id)
        {
            Product product = repository.Products.FirstOrDefault(p => p.Id == id);
            
            ViewBag.CategoryList = GetCategorySelectList();
            return View(product);
        }

        public ActionResult CategoryEdit(int id)
        {
            Category category = repository.Categories.FirstOrDefault(c => c.Id == id);
            return View(category);
            
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
        [HttpPost]
        public ActionResult CategoryEdit(Category category)
        {
            if (ModelState.IsValid)
            {
                repository.SaveCategory(category);
                TempData["msg"] = string.Format("{0} 保存成功", category.Name);
                return RedirectToAction("CategoryIndex");
            }
            else {
                return View(category);
            }
             
        }

        public ActionResult Create()
        {
            IEnumerable<SelectListItem> selectListItem = repository.Categories.ToList().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            ViewBag.CategoryList = selectListItem;
            return View("Edit",new Product());
        }
        public ActionResult CategoryCreate()
        {
            return View("CategoryEdit",new Category());
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Product deletedProduct = repository.DeleteProduct(id);
            if (deletedProduct != null)
            {
                TempData["msg"] = string.Format("{0} 产品信息删除成功",deletedProduct.Name);

            }
            return RedirectToAction("Index");
        }
        public ActionResult CategoryDelete(int id)
        {
            Category deletedCategory = repository.DeleteCategory(id);
            if (deletedCategory != null)
            {
                TempData["msg"] = string.Format("{0} 分类信息删除成功", deletedCategory.Name);

            }
            return RedirectToAction("CategoryIndex");

        }


    }
}