using Business.Concrete;
using Business.ValidationRules.FluentValidation;
using DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductTrackingSystem.Controllers
{
    public class ContactController : Controller
    {
        ContactManager cm = new ContactManager(new EfContactDal());
        ContactValidator cv = new ContactValidator();
        public ActionResult Index(string p)
        {
            var values = cm.GetAllSearch(p);
            if (string.IsNullOrEmpty(p))
            {
                var contactValues = cm.GetAll();
                return View(contactValues);
            }
            return View(values);
        }
        public ActionResult GetContactDetails(int id)
        {
            var contactValue = cm.GetById(id);
            return View(contactValue);
        }
        public PartialViewResult MessageListMenu()
        {
            return PartialView();
        }
    }
}