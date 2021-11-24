using Business.Concrete;
using DataAccess.EntityFramework;
using ProductTrackingSystem.DataAccess;
using ProductTrackingSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductTrackingSystem.Controllers
{
    [AllowAnonymous]
    public class SalesController : Controller
    {
        SalesManager sm = new SalesManager(new EfSalesDal());
        ProductTrackingDbContext c = new ProductTrackingDbContext();
        public ActionResult Index()
        {
            var model = sm.GetAll();
            return View(model);
        }

        public ActionResult Buy(int id)
        {
            var model = c.Carts.FirstOrDefault(x => x.CartId == id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Buys(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = c.Carts.FirstOrDefault(x => x.CartId==id);
                    var product = c.Products.FirstOrDefault(x => x.ProductId == model.ProductId);
                    product.UnitsInStock = product.UnitsInStock - model.Quantity;
                    var sales = new Sales
                    {
                        UserId = model.UserId,
                        ProductId = model.ProductId,
                        CartId = model.CartId,
                        UnitPrice = model.UnitPrice,
                        Quantity = model.Quantity,
                        TotalPrice = model.TotalPrice,
                        Date = DateTime.Now
                    };
                    c.Carts.Remove(model);
                    c.Sales.Add(sales);
                    c.SaveChanges();
                    ViewBag.islem = "Satın alma işlemi başarılı bir şekilde gerçekleşmiştir";
                }
            }
            catch (Exception)
            {

                ViewBag.islem = "Satın alma işlemi başarısız!";
            }
            return View("islem");
        }
        public ActionResult AllBuy(decimal? Tutar)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var user = c.Users.FirstOrDefault(x => x.Email == userName);
                var model = c.Carts.Where(x => x.UserId == user.UserId).ToList();
                var userId = c.Carts.FirstOrDefault(x => x.UserId == user.UserId);
                if (model != null)
                {
                    if (userId == null)
                    {
                        ViewBag.tutar = "Sepetinizde Ürün Bulunmamaktadır.";
                    }
                }
                else if (userId != null)
                {
                    Tutar = c.Carts.Where(x => x.UserId == userId.UserId).Sum(x => x.TotalPrice);
                    TempData["tutar"] = "Toplam Fiyat: " + Tutar + " ₺";
                }
                return View(model);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult AllBuys()
        {
            var userMail = User.Identity.Name;
            var user = c.Users.FirstOrDefault(x => x.Email == userMail);
            var model = c.Carts.Where(x => x.UserId == user.UserId).ToList();
            int row = 0;
            foreach (var item in model)
            {
                var sales = new Sales
                {
                    UserId = model[row].UserId,
                    ProductId = model[row].ProductId,
                    CartId = model[row].CartId,
                    UnitPrice = model[row].UnitPrice,
                    Quantity = model[row].Quantity,
                    TotalPrice = model[row].TotalPrice,
                    Date = DateTime.Now
                };
                c.Sales.Add(sales);
                c.SaveChanges();
                row ++;
            }
            foreach (var item in model)
            {
                var product = c.Products.FirstOrDefault(x => x.ProductId == item.ProductId);
                if (product!=null)
                {
                    product.UnitsInStock = product.UnitsInStock - item.Quantity;
                }
            }
            c.Carts.RemoveRange(model);
            c.SaveChanges();
            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}