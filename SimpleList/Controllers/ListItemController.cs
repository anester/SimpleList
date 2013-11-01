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
using SimpleListLogic.Managers;

namespace SimpleList.Controllers
{
    public class ListItemController : SecureController<ListItemManager>
    {
        private ListDbContext db = new ListDbContext();

        //
        // GET: /ListItem/

        public ActionResult Index(string username = "", int userlistid = 0)
        {
            IEnumerable<ListItem> listitems = null;
            if (!string.IsNullOrEmpty(username) && userlistid == 0)
            {
                //listitems = db.ListItems.Where(li => li.List.Owner.UserLogin == username).Include(l => l.List);
            }
            else if (!string.IsNullOrEmpty(username) && userlistid != 0)
            {
                //listitems = db.ListItems.Where(li => li.List.Owner.UserLogin == username && li.List.UserListId == userlistid).Include(l => l.List);
            }
            else
            {
                listitems = db.ListItems.Include(l => l.List);
            }

            return View(listitems.ToList());
        }

        public ActionResult ListItemsPart(int id)
        {
            IEnumerable<ListItem> items = Manager.GetItems(id);
            return PartialView("ListItemsPart", items);
        }

        public ActionResult CompleteListItem(int id)
        {
            //figure out how to handle error gracefully
            //for example what if the id doesn't exists
            if (Manager.MarkAsDone(id))
            {
                IEnumerable<ListItem> items = Manager.GetItemsFromListItemId(id);
                return PartialView("ListItemsPart", items);
            }
            return PartialView("ListItemsPart", null);
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
            //Figure out what the hell todo here, since I am calling this method
            //with jquery do not need to return full page
            if(Manager.DeleteItem(id))
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}