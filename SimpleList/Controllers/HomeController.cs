using SimpleListData;
using SimpleListLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleList.Controllers
{
    public class HomeController : SecureController<LoginManager>
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(User user)
        {
            SimpleListContext.ListDbContext context = new SimpleListContext.ListDbContext();
            User luser = context.Users.FirstOrDefault(u => u.UserLogin == user.UserLogin);
            if (luser != null)
            {
                Session["USER"] = luser;
                return Redirect("/List/" + user.UserLogin);
            }

            return Redirect("/Users/Create");
        }
    }
}
