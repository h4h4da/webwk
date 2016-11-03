using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyStore.Domain.Abstract;
using MyStore.Domain.Concrete;
using MyStore.WebUI.Models;
using PagedList;
namespace MyStore.WebUI.Controllers
{
    public class ProductController : Controller
    {

        private IProductsRepository repository;

        public int PageSize = 2;

        public ProductController(IProductsRepository p)
        {
            repository = p;
        }
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult List(int page=1)
        //{

        //    ProductsListViewModel viewModel = new ProductsListViewModel {
        //        Products = reopository.Products.OrderBy(P => P.Id).Skip((page - 1) * PageSize).Take(PageSize),
        //        paginginfo = new PagingInfo {
        //            CurrentPage = page,
        //            ItemsPerPage=PageSize,
        //            TotalItems=reopository.Products.Count()
        //        }
        //    };
        //    return View(viewModel);
        //}


        public ActionResult List2(int categoryId = 0, int page = 1) {

            IQueryable<Product> productlist = repository.Products;

            if (categoryId > 0)
            {
                productlist = repository.Products.Where(p => p.CategoryId == categoryId);
            }
            var s= productlist.OrderBy(x=>x.Id);
           
            IPagedList<Product> pagedList = s.ToPagedList(page, 2);
            return View(pagedList);
        }
        public ActionResult List(int categoryId=0,int page = 1)
        {

            IQueryable<Product> productlist = repository.Products;

            if (categoryId > 0)
            {
                productlist = repository.Products.Where(p=>p.CategoryId==categoryId);
            }

            ProductsListViewModel viewModel = new ProductsListViewModel
            {
                Products = productlist.OrderBy(P => P.Id).Skip((page - 1) * PageSize).Take(PageSize),
                paginginfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = productlist.Count()
                },
                CurrentCategoryId = categoryId
            };
            return View(viewModel);
        }
    }
}