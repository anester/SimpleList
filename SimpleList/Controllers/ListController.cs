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
        public ActionResult Index(string loginname)
        {
            IEnumerable<UserList> lists = Manager.GetUserLists(loginname);
            return View(lists);
        }

        public PartialViewResult Create(string loginname)
        {
            UserList list = new UserList();
            return PartialView("CreateListPart", list);
        }

        [HttpPost]
        public ActionResult Create(UserList list)
        {
            UserList nlist  = Manager.CreateUserList(ListSession.CurrentLoginName, list.UserListName, list.Description);
            return PartialView("CreateListPart", nlist);
        }
    }
}
