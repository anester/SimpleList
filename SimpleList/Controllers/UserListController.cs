using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleListData;
using SimpleListContext;
using SimpleListLogic.Web;

namespace SimpleList.Controllers
{
    public class UserListController : BaseController
    {
        private ListDbContext db = new ListDbContext();

        //
        // GET: /UserList/

        public ActionResult Index()
        {
            var userlists = db.UserLists.Include(u => u.Owner);
            return View(userlists.ToList());
        }

        //
        // GET: /UserList/Details/5

        public ActionResult Details(int id = 0)
        {
            UserList userlist = db.UserLists.Find(id);
            if (userlist == null)
            {
                return HttpNotFound();
            }
            return View(userlist);
        }

        //
        // GET: /UserList/Create

        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserLogin");
            return View();
        }

        //
        // POST: /UserList/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserList userlist)
        {
            if (ModelState.IsValid)
            {
                db.UserLists.Add(userlist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserLogin", userlist.UserId);
            return View(userlist);
        }

        //
        // GET: /UserList/Edit/5

        public ActionResult Edit(int id = 0)
        {
            UserList userlist = db.UserLists.Find(id);
            if (userlist == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserLogin", userlist.UserId);
            return View(userlist);
        }

        //
        // POST: /UserList/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserList userlist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userlist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserLogin", userlist.UserId);
            return View(userlist);
        }

        //
        // GET: /UserList/Delete/5

        public ActionResult Delete(int id = 0)
        {
            UserList userlist = db.UserLists.Find(id);
            if (userlist == null)
            {
                return HttpNotFound();
            }
            return View(userlist);
        }

        //
        // POST: /UserList/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserList userlist = db.UserLists.Find(id);
            db.UserLists.Remove(userlist);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}