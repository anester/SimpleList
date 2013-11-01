using SimpleListLogic.Managers;
using SimpleListLogic.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleList
{
    public class BaseController : Controller
    {

    }

    [SessionRequired]
    public class SecureController<T> : BaseController
        where T : BaseManager
    {
        public T Manager { get; set; }
        public ISimpleListSession ListSession { get; set; }

        public SecureController()
        {
            ListSession = new SimpleListSession();
            Manager = (T)Activator.CreateInstance(typeof(T), ListSession);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.User = ListSession.CurrentLoginName;
            base.OnActionExecuting(filterContext);
        }

    }
}
