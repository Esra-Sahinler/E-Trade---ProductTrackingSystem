using Business.Concrete;
using Business.ValidationRules.FluentValidation;
using DataAccess.EntityFramework;
using FluentValidation.Results;
using ProductTrackingSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductTrackingSystem.Controllers
{
    public class UserPanelMessageController : Controller
    {
        MessageManager mm = new MessageManager(new EfMessageDal());
        MessageValidator messageValidator = new MessageValidator();
        public ActionResult Inbox()
        {
            string p = (string)Session["Email"];
            var messageList = mm.GetAllInbox(p);
            return View(messageList);
        }
        public ActionResult Sendbox()
        {
            string p = (string)Session["Email"];
            var messageList = mm.GetAllSendbox(p);
            return View(messageList);
        }

        public ActionResult GetInBoxMessageDetails(int id)
        {
            var messageValue = mm.GetById(id);
            return View(messageValue);
        }

        public ActionResult GetSendBoxMessageDetails(int id)
        {
            var messageValue = mm.GetById(id);
            return View(messageValue);
        }
        public PartialViewResult MessageListMenu()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewMessage(Message m)
        {
            string sender = (string)Session["Email"];
            ValidationResult results = messageValidator.Validate(m);
            if (results.IsValid)
            {
                m.SenderMail = sender;
                m.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                mm.Add(m);
                return RedirectToAction("Sendbox");
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
    }
}