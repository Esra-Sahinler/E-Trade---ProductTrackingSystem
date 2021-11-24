using Business.Concrete;
using DataAccess.EntityFramework;
using ProductTrackingSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductTrackingSystem.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        ProductManager pm = new ProductManager(new EfProductDal());
        ProductTrackingDbContext c = new ProductTrackingDbContext();
        public ActionResult Index(string p)
        {
            var values = pm.GetAllSearch(p);
            if (string.IsNullOrEmpty(p))
            {
                var productValues = pm.GetAll();
                return View(productValues);
            }
            return View(values);
        }
        public ActionResult ProductsByCategory(int id)
        {
            var model = c.Products.Where(x => x.Category.CategoryId == id).ToList();
            var category = c.Categories.Where(x => x.CategoryId == id).Select(x => x.CategoryName).FirstOrDefault();
            ViewBag.viewCategory = category;
            return View(model);
        }
        public ActionResult ProductsByBrand(int id)
        {
            var model = c.Products.Where(x => x.Brand.BrandId == id).ToList();
            var brand = c.Brands.Where(x => x.BrandId == id).Select(x => x.BrandName).FirstOrDefault();
            ViewBag.viewBrand = brand;
            return View(model);
        }
        public ActionResult ProductsByColor(int id)
        {
            var model = c.Products.Where(x => x.Color.ColorId == id).ToList();
            var color = c.Colors.Where(x => x.ColorId == id).Select(x => x.ColorName).FirstOrDefault();
            ViewBag.viewColor = color;
            return View(model);
        }

        public ActionResult ProductDetails(int id)
        {
            var productValues = pm.GetById(id);
            return View(productValues);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult HomePage()
        {
            return View();
        }
    }
}