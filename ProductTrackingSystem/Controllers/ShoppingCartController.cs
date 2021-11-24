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
    public class ShoppingCartController : Controller
    {
        CartManager cm = new CartManager(new EfCartDal());
        //UserManager um = new UserManager(new EfUserDal());
        //ProductManager pm = new ProductManager(new EfProductDal());
        ProductTrackingDbContext c = new ProductTrackingDbContext();
        public ActionResult Index(decimal? Tutar)
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
                else if(userId != null)
                {
                    Tutar = c.Carts.Where(x => x.UserId == userId.UserId).Sum(x => x.TotalPrice);
                    TempData["tutar"] = "Toplam Fiyat: " + Tutar + " ₺"; 
                }
                return View(model);
            }
            return HttpNotFound();
        }

        public ActionResult AddCartProduct(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var model = c.Users.FirstOrDefault(x => x.Email == userName);
                var product = c.Products.Find(id);
                var cart = c.Carts.FirstOrDefault(x => x.UserId == model.UserId && x.ProductId == id);
                if (model != null)
                {
                    if (cart != null)
                    {
                        cart.Quantity++;
                        cart.TotalPrice = product.UnitPrice * cart.Quantity;
                        c.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    var ca = new Cart
                    {
                        UserId = model.UserId,
                        ProductId = product.ProductId,
                        Quantity = 1,
                        UnitPrice = product.UnitPrice,
                        TotalPrice = product.UnitPrice,
                        Date = DateTime.Now
                    };
                    c.Entry(ca).State = System.Data.Entity.EntityState.Added;
                    c.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return HttpNotFound();
        }

        public ActionResult Decrease(int id)
        {
            var model = cm.GetById(id);
            if (model.Quantity==1)
            {
                cm.Delete(model);
                return RedirectToAction("Index");
            }
            model.Quantity--;
            model.TotalPrice = model.Quantity * model.UnitPrice;
            cm.Update(model);
            return RedirectToAction("Index");
        }
        public ActionResult Increase(int id)
        {
            var model = cm.GetById(id);
            model.Quantity++;
            model.TotalPrice = model.Quantity * model.UnitPrice;
            cm.Update(model);
            return RedirectToAction("Index");
        }

        public void DynamicQuantity(int id, int quantity)
        {
            var model = cm.GetById(id);
            model.Quantity = quantity;
            model.TotalPrice = model.UnitPrice * model.Quantity;
            cm.Update(model);
        }

        public ActionResult TotalCount(int? count)
        {
            if (User.Identity.IsAuthenticated)
            {
                var model = c.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
                count = c.Carts.Where(x => x.UserId == model.UserId).Count();
                ViewBag.Count = count;
                if (count==0)
                {
                    ViewBag.Count = "";
                }
                return PartialView();
            }
            return HttpNotFound();
        }

        public ActionResult Delete(int id)
        {
            var model = cm.GetById(id);
            cm.Delete(model);
            return RedirectToAction("Index");
        }

        public ActionResult AllDelete()
        {
            var userMail = User.Identity.Name;
            var model = c.Users.FirstOrDefault(x => x.Email.Equals(userMail));
            var delete = c.Carts.Where(x => x.UserId.Equals(model.UserId));
            c.Carts.RemoveRange(delete);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteSelectedProducts(FormCollection form)
        {
            string[] selectId = form["select_id"].Split(new char[] { ',' });
            foreach (string item in selectId)
            {
                Cart model = c.Carts.Find(int.Parse(item));
                c.Carts.Remove(model);
                c.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult BuySelectedProducts(List<Cart> data)
        {
            string[] ids = data.Select(x => x.CartId.ToString()).ToArray();
            decimal total = 0;
            foreach (var item in ids)
            {
                var model = c.Carts.Find(int.Parse(item));
                if (int.Parse(item)!=0)
                {
                    total += model.TotalPrice;
                }
            }
            ViewBag.Total = total.ToString("0.00") + "₺";
            return View(data);
        }
        [HttpPost]
        public ActionResult BuySelectedProducts2(int[] ids)
        {
            var model = c.Carts.Where(x => ids.Contains(x.CartId)).ToList();
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
                row++;
            }
            foreach (var item in model)
            {
                var product = c.Products.FirstOrDefault(x => x.ProductId == item.ProductId);
                if (product != null)
                {
                    product.UnitsInStock = product.UnitsInStock - item.Quantity;
                }
            }
            c.Carts.RemoveRange(model);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}