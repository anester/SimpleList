﻿using SimpleListData;
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
            ViewBag.IsDetail = false;
            IEnumerable<UserList> lists = Manager.GetUserLists(loginname).Where(l => !l.DateCompleted.HasValue);
            return View("Index", lists);
        }

        public ActionResult Details(int id = 0)
        {
            UserList list = Manager.GetUserList(id);
            ViewBag.IsDetail = true;
            return View("Index", list);
        }

        [HttpPost,
        ActionName("Index")]
        public ActionResult IndexPart(string loginname, string daterange = "", string name = "")
        {
            DateTime d1 = DateTime.MinValue;
            DateTime d2 = DateTime.MaxValue;

            //Move logic to Logic layer
            if (daterange.Contains(".."))
            {
                d1 = DateTime.Parse(daterange.Split(new string[] { ".." }, StringSplitOptions.RemoveEmptyEntries)[0]);
                d2 = DateTime.Parse(daterange.Split(new string[] { ".." }, StringSplitOptions.RemoveEmptyEntries)[1]);
            }

            IEnumerable<UserList> lists = Manager.GetUserLists(loginname, name, d1, d2);
            return PartialView("ListOfUserListPart", lists);
        }

        public ActionResult MyLists()
        {
            return Index(ListSession.CurrentLoginName);
        }

        public ActionResult OpenList(int id)
        {
            UserList list = Manager.OpenUserList(id);
            return PartialView("ListPart", list);
        }

        public ActionResult LockList(int id)
        {
            UserList list = Manager.LockUserList(id);
            return PartialView("ListPart", list);
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
