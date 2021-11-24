using Business.Concrete;
using DataAccess.EntityFramework;
using ProductTrackingSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace ProductTrackingSystem.Controllers
{
    public class ProductController : Controller
    {
        ProductManager pm = new ProductManager(new EfProductDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());

        public ActionResult Index(int p = 1)
        {
            var productValues = pm.GetAll().ToPagedList(p, 4);
            return View(productValues);
        }

        public ActionResult ProductReport()
        {
            var productValues = pm.GetAll();
            return View(productValues);
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            List<SelectListItem> valueCategory = (from x in cm.GetAll()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryId.ToString()
                                                  }).ToList();
            ViewBag.vc = valueCategory;
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            product.Date = DateTime.Parse(DateTime.Now.ToShortDateString());
            pm.Add(product);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            List<SelectListItem> valueCategory = (from x in cm.GetAll()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryId.ToString()
                                                  }).ToList();
            ViewBag.vc = valueCategory;
            var productValue = pm.GetById(id);
            return View(productValue);
        }
        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            pm.Update(product);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteProduct(int id)
        {
            var productValue = pm.GetById(id);
            //productValue.ProductStatus=false
            pm.Delete(productValue);
            return RedirectToAction("Index");
        }
    }
}