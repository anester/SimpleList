﻿using SimpleListLogic.Managers;
using SimpleListLogic.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
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

        protected override void OnAuthentication(System.Web.Mvc.Filters.AuthenticationContext filterContext)
        {
            base.OnAuthentication(filterContext);
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.User = ListSession.CurrentLoginName;
            base.OnActionExecuting(filterContext);
        }
    }

    [SessionRequired]
    public class SecureApiController<T> : ApiController
    {
        public T Manager { get; set; }
        public ISimpleListSession ListSession { get; set; }

        public SecureApiController()
        {
            ListSession = new SimpleListSession();
            Manager = (T)Activator.CreateInstance(typeof(T), ListSession);
        }
    }
}
