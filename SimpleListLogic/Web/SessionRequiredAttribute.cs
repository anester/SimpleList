using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SimpleListLogic.Web
{
    public class SessionRequiredAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["UserID"] == null)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    // For AJAX link call, the redirect is done from the javascript on session timeout.
                    filterContext.Result = new JsonResult { Data = "TimeOut" };
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary { { "area", "" }, { "controller", "Home" }, { "action", "Index" } }
                    );
                }
            }
        }
    }
}
