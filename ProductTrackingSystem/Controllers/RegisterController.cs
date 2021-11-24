using Business.Concrete;
using DataAccess.EntityFramework;
using ProductTrackingSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductTrackingSystem.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        UserManager um = new UserManager(new EfUserDal());
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserRegister()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserRegister(User user)
        {
            um.Add(user);
            return RedirectToAction("UserLogin", "Login");
        }

        [HttpGet]
        public ActionResult OrderRegister()
        {
            return View();
        }
        [HttpPost]
        public ActionResult OrderRegister(User user)
        {
            user.About = "Müşteri";
            user.RoleId = 1;
            um.Add(user);
            return RedirectToAction("OrderLogin", "Login");
        }
    }
}