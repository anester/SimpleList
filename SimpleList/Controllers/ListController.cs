using SimpleListData;
using SimpleListLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleList.Controllers
{
    public class ListController : SecureController<UserListManager>
    {
        //
        // GET: /List/
        public ActionResult Index(string loginname, int listid = 0)
        {
            //Move logic to Logic layer
            IEnumerable<UserList> lists = Manager.GetUserLists(loginname).Where(l => !l.DateCompleted.HasValue);
            return View("Index", lists);
        }

        public ActionResult MyLists()
        {
            return Index(ListSession.CurrentLoginName);
        }

        public ActionResult CloseList(int id)
        {
            UserList list = Manager.CloseUserList(id);
            return PartialView("ListPart", list);
        }

        public PartialViewResult ListPart(int id)
        {
            UserList list = Manager.GetUserList(id);
            return PartialView("ListPart", list);
        }

        public PartialViewResult Create(string loginname)
        {
            UserList list = new UserList();
            return PartialView("CreateListPart", list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserList list)
        {
            UserList nlist = Manager.CreateUserList(ListSession.CurrentLoginName, list.UserListName, list.Description);
            return PartialView("CreateListPart", nlist);
        }
    }
}
