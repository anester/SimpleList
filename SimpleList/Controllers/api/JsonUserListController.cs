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
    public class JsonUserListController : SecureApiController<UserListManager>
    {
        private ListDbContext db = new ListDbContext();

        // GET api/JsonUserList
        public IEnumerable<UserList> GetUserLists()
        {
            var userlists = db.UserLists.Include(u => u.Owner);
            return userlists.AsEnumerable();
        }

        // GET api/JsonUserList/5
        public UserList GetUserList(int id)
        {
            UserList userlist = db.UserLists.Find(id);
            if (userlist == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return userlist;
        }

        // PUT api/JsonUserList/5
        public HttpResponseMessage PutUserList(int id, UserList userlist)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != userlist.UserListId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(userlist).State = EntityState.Modified;

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

        // POST api/JsonUserList
        public HttpResponseMessage PostUserList(UserList userlist)
        {
            if (ModelState.IsValid)
            {
                db.UserLists.Add(userlist);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, userlist);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = userlist.UserListId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/JsonUserList/5
        public HttpResponseMessage DeleteUserList(int id)
        {
            UserList userlist = db.UserLists.Find(id);
            if (userlist == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.UserLists.Remove(userlist);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, userlist);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}