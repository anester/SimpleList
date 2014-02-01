using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using SimpleListData;
using SimpleListContext;
using SimpleListLogic.Managers;

namespace SimpleList.Controllers
{
    public class JsonListItemController : SecureApiController<ListItemManager>
    {
        private ListDbContext db = new ListDbContext();

        // GET api/JsonListItem
        public IEnumerable<ListItem> GetListItems()
        {
            var listitems = db.ListItems.Include(l => l.List);
            return listitems.AsEnumerable();
        }

        // GET api/JsonListItem/5
        public ListItem GetListItem(int id)
        {
            ListItem listitem = db.ListItems.Find(id);
            if (listitem == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return listitem;
        }

        // PUT api/JsonListItem/5
        public HttpResponseMessage PutListItem(int id, ListItem listitem)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != listitem.ListItemId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(listitem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/JsonListItem
        public HttpResponseMessage PostListItem(ListItem listitem)
        {
            if (ModelState.IsValid)
            {
                db.ListItems.Add(listitem);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, listitem);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = listitem.ListItemId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/JsonListItem/5
        public HttpResponseMessage DeleteListItem(int id)
        {
            ListItem listitem = db.ListItems.Find(id);
            if (listitem == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.ListItems.Remove(listitem);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, listitem);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}