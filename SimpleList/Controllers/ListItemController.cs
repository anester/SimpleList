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
    public class ListItemController : BaseController
    {
        private ListDbContext db = new ListDbContext();

        //
        // GET: /ListItem/

        public ActionResult Index(string username = "", int userlistid = 0)
        {
            IEnumerable<ListItem> listitems = null;
            if (!string.IsNullOrEmpty(username) && userlistid == 0)
            {
                listitems = db.ListItems.Where(li => li.List.Owner.UserLogin == username).Include(l => l.List);
            }
            else if (!string.IsNullOrEmpty(username) && userlistid != 0)
            {
                listitems = db.ListItems.Where(li => li.List.Owner.UserLogin == username && li.List.UserListId == userlistid).Include(l => l.List);
            }
            else
            {
                listitems = db.ListItems.Include(l => l.List);
            }

            return View(listitems.ToList());
        }

        //
        // GET: /ListItem/Details/5

        public ActionResult Details(int id = 0)
        {
            ListItem listitem = db.ListItems.Find(id);
            if (listitem == null)
            {
                return HttpNotFound();
            }
            return View(listitem);
        }

        //
        // GET: /ListItem/Create

        public ActionResult Create()
        {
            ViewBag.UserListId = new SelectList(db.UserLists, "UserListId", "UserListName");
            return View();
        }

        //
        // POST: /ListItem/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ListItem listitem)
        {
            if (ModelState.IsValid)
            {
                db.ListItems.Add(listitem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserListId = new SelectList(db.UserLists, "UserListId", "UserListName", listitem.UserListId);
            return View(listitem);
        }

        //
        // GET: /ListItem/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ListItem listitem = db.ListItems.Find(id);
            if (listitem == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserListId = new SelectList(db.UserLists, "UserListId", "UserListName", listitem.UserListId);
            return View(listitem);
        }

        //
        // POST: /ListItem/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ListItem listitem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(listitem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserListId = new SelectList(db.UserLists, "UserListId", "UserListName", listitem.UserListId);
            return View(listitem);
        }

        //
        // GET: /ListItem/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ListItem listitem = db.ListItems.Find(id);
            if (listitem == null)
            {
                return HttpNotFound();
            }
            return View(listitem);
        }

        //
        // POST: /ListItem/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ListItem listitem = db.ListItems.Find(id);
            db.ListItems.Remove(listitem);
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