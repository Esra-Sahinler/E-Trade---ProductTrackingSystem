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
    public class ChartController : Controller
    {
        ProductManager pm = new ProductManager(new EfProductDal());
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GoogleChart()
        {
            var values = pm.GetAll();
            var model = new
            {
                productName = values.FirstOrDefault().ProductName,
                unitsInStock = values.FirstOrDefault().UnitsInStock
            };

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        


        //public List<Product> chartList()
        //{
        //    //List<Product> products = new List<Product>();
        //    //using (var context = new ProductTrackingDbContext())
        //    //{
        //    //    products = context.Products.Select(x => new Product()
        //    //    {
        //    //        ProductName = x.ProductName,
        //    //        UnitsInStock = x.UnitsInStock
        //    //    }).ToList();
        //    //}



        //    //List<Product> valueCategory = (from x in pm.GetAll()
        //    //                               select new Product()
        //    //                               {
        //    //                                   ProductName = x.ProductName,
        //    //                                   UnitsInStock = x.UnitsInStock
        //    //                               }).ToList();
        //    return valueCategory;
        //}












        //public List<Product> Veriler()
        //{
        //    //var values = pm.GetAll();
        //    //var model = new
        //    //{
        //    //    ProductName = values.FirstOrDefault().ProductName,
        //    //    UnitsInStock = values.FirstOrDefault().UnitsInStock
        //    //};

        //    List<Product> pd = new List<Product>();
        //    ProductTrackingDbContext db = new ProductTrackingDbContext();
        //    pd = db.Products.Select(x => new Product
        //    {
        //        ProductName = x.ProductName,
        //        UnitsInStock = x.UnitsInStock
        //    }).ToList();

        //    return pd;
        //}
    }
}