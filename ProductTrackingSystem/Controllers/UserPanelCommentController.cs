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
    public class UserPanelCommentController : Controller
    {
        CommentManager com = new CommentManager(new EfCommentDal());
        public ActionResult MyComment(string p)
        {
            ProductTrackingDbContext c = new ProductTrackingDbContext();
            p = (string)Session["Email"];
            var userIdInfo = c.Users.Where(x => x.Email == p).Select(y=>y.UserId).FirstOrDefault();
            var commentValues = com.GetAllByUser(userIdInfo);
            return View(commentValues);
        }
    }
}