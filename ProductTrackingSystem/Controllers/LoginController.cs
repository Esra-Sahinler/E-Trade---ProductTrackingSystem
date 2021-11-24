using Business.Concrete;
using DataAccess.EntityFramework;
using ProductTrackingSystem.DataAccess;
using ProductTrackingSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProductTrackingSystem.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        UserLoginManager ulm = new UserLoginManager(new EfUserDal());
        ProductTrackingDbContext c = new ProductTrackingDbContext();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Admin admin)
        {
            
            var adminuserinfo = c.Admins.FirstOrDefault(x => x.AdminUserName == admin.AdminUserName && x.AdminPassword == admin.AdminPassword);
            if (adminuserinfo!= null)
            {
                FormsAuthentication.SetAuthCookie(adminuserinfo.AdminUserName,false);
                Session["AdminUserName"] = adminuserinfo.AdminUserName;
                return RedirectToAction("Index","AdminCategory");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult UserLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserLogin(User user)
        {
            //ProductTrackingDbContext c = new ProductTrackingDbContext();
            //var userinfo = c.Users.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);
            var userinfo = ulm.GetUser(user.Email, user.Password);
            if (userinfo != null)
            {
                FormsAuthentication.SetAuthCookie(userinfo.Email, false);
                Session["Email"] = userinfo.Email;
                return RedirectToAction("UserProfile", "UserPanel");
            }
            else
            {
                return RedirectToAction("UserLogin");
            }
        }

        [HttpGet]
        public ActionResult OrderLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult OrderLogin(User user)
        {
            var userInfo = ulm.GetUser(user.Email, user.Password);
            if (userInfo != null)
            {
                FormsAuthentication.SetAuthCookie(userInfo.Email, false);
                Session["Email"] = userInfo.Email;
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("OrderLogin");
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            //Session.Abandon();
            return RedirectToAction("HomePage", "Home");
        }

        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(User user)
        {
            var userInfo = c.Users.FirstOrDefault(x => x.Email == user.Email);
            if (userInfo != null)
            {
                Guid random = Guid.NewGuid();
                userInfo.Password = random.ToString().Substring(0, 8);
                c.SaveChanges();
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("deneme@gmail.com", "Şifre Sıfırlama");
                mail.To.Add(userInfo.Email);
                mail.IsBodyHtml = true;
                mail.Subject = "Şifre Değiştirme İsteği";
                mail.Body += "Merhaba " + userInfo.Name + " " + userInfo.Surname + "<br/> Mailiniz: " + userInfo.Email + "<br/> Şifreniz: " + userInfo.Password;
                NetworkCredential network = new NetworkCredential("deneme@gmail.com", "deneme");
                client.Credentials = network;
                client.Send(mail);
                return RedirectToAction("UserLogin");
            }
            ViewBag.hata = "Böyle bir mail adresi bulunamadı.";
            return View();
        }
    }
}