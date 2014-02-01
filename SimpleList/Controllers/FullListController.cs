using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SimpleListData;
using SimpleListContext;
using SimpleListLogic.Managers;

namespace SimpleList.Controllers
{
    public class FullListController : SecureController<UserListManager>
    {
        // GET: /FullList/
        public ActionResult Index()
        {
            var userlists = Manager.GetUserLists(ListSession.CurrentLoginName);
            return View(userlists.ToList());
        }

    }
}
