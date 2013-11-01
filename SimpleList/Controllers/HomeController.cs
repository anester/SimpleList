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
            //Should only get here if you are logged in
            return Redirect("/Login/Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Simple List App.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
