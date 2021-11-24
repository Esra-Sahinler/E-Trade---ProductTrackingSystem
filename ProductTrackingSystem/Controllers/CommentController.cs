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
    public class CommentController : Controller
    {
        CommentManager com = new CommentManager(new EfCommentDal());
        public ActionResult Index(int p =1 )
        {
            var commentValues = com.GetAll().ToPagedList(p,10);
            return View(commentValues);
        }

        [HttpGet]
        public ActionResult AddComment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddComment(Comment comment)
        {
            com.Add(comment);
            return RedirectToAction("Index");
        }
        public ActionResult CommentByProduct(int id)
        {
            var commentValues = com.GetAllByProductId(id);
            return View(commentValues);
        }
    }
}