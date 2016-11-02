using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyStore.Domain.Concrete;
using MyStore.Domain.Abstract;
namespace MyStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        // GET: Nav

        private IProductsRepository repository;
        public NavController(IProductsRepository proRepositiry)
        {
            this.repository = proRepositiry;
        }
        public PartialViewResult Menu(int categoryId=0)
        {
            ViewBag.CurrentCategoryId = categoryId;
            IEnumerable<Category> categories = repository.Categories.ToList();
            return PartialView(categories);
        }
    }
}