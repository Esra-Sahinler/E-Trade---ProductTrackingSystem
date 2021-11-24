using Business.Concrete;
using Business.ValidationRules.FluentValidation;
using DataAccess.EntityFramework;
using FluentValidation.Results;
using ProductTrackingSystem.DataAccess;
using ProductTrackingSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductTrackingSystem.Controllers
{
    public class UserPanelController : Controller
    {
        ProductManager pm = new ProductManager(new EfProductDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        UserManager um = new UserManager(new EfUserDal());
        ProductTrackingDbContext c = new ProductTrackingDbContext();
        UserValidator userValidator = new UserValidator();

        [HttpGet]
        public ActionResult UserProfile(int id=0)
        {
            string p = (string)Session["Email"];
            id = c.Users.Where(x => x.Email == p).Select(y => y.UserId).FirstOrDefault();
            var userValues = um.GetById(id);
            return View(userValues);
        }
        [HttpPost]
        public ActionResult UserProfile(User user)
        {
            ValidationResult results = userValidator.Validate(user);
            if (results.IsValid)
            {
                um.Update(user);
                return RedirectToAction("UserProfile");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        public ActionResult MyProduct()
        {
            var values = pm.GetAll();
            return View(values);
        }

        [HttpGet]
        public ActionResult NewProduct()
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
        public ActionResult NewProduct(Product product)
        {
            product.Date = DateTime.Parse(DateTime.Now.ToShortDateString());
            pm.Add(product);
            return RedirectToAction("MyProduct");
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
            return RedirectToAction("MyProduct");
        }

        public ActionResult DeleteProduct(int id)
        {
            var productValue = pm.GetById(id);
            //productValue.ProductStatus=false
            pm.Delete(productValue);
            return RedirectToAction("MyProduct");
        }
    }
}