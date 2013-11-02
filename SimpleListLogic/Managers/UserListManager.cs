using SimpleListData;
using SimpleListLogic.Web;
using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimpleListLogic.Managers
{
    public class UserListManager : BaseManager
    {
        public UserListManager(ISimpleListSession session)
            : base(session)
        {

        }

        public IEnumerable<UserList> GetUserLists(string username = "")
        {
            string lookupname = string.IsNullOrEmpty(username) ? Session.CurrentLogin.LoginName : username;
            var lists = Context.UserLists.Where(ul => ul.Owner.UserLogin.LoginName == lookupname).OrderByDescending(l => l.DateCreated);
            return lists;
        }

        public IEnumerable<UserList> GetUserLists(string username, string listname, DateTime? fromdate, DateTime? todate)
        {
            string lookupname = string.IsNullOrEmpty(username) ? Session.CurrentLogin.LoginName : username;
            string lookuplistname = "";
            DateTime lookupfromdate = fromdate.HasValue ? fromdate.Value : DateTime.MinValue;
            DateTime lookuptodate = todate.HasValue ? todate.Value : DateTime.MaxValue;
            if (!string.IsNullOrEmpty(listname))
            {
                lookuplistname = listname;
            }

            var lists = from l in Context.UserLists
                        where l.UserListName.Contains(lookuplistname)
                            && l.DateCreated >= lookupfromdate 
                            && l.DateCreated <= lookuptodate
                            && l.ListStatus != UserListStatus.Closed
                        orderby l.DateCreated descending
                        select l;
            
            //var lists = Context.UserLists.Where(ul => ul.Owner.UserLogin.LoginName == lookupname).OrderByDescending(l => l.DateCreated);
            return lists;
        }

        public UserList CreateUserList(string username, string listname, string description = "")
        {
            int id = Context.Logins.Where(l => l.LoginName == username).Select(l => l.LoginId).FirstOrDefault();
            UserList list = new UserList
            {
                UserId = id,
                UserListName = listname,
                Description = description,
                DateCreated = DateTime.Now
            };

            Context.UserLists.Add(list);
            Context.SaveChanges();

            return list;
        }

        public UserList GetUserList(int id)
        {
            return Context.UserLists.FirstOrDefault(l => l.UserListId == id);
        }

        public UserList CloseUserList(int id)
        {
            UserList list = GetUserList(id);
            list.DateCompleted = DateTime.Now;
            list.ListStatus = UserListStatus.Closed;
            Context.SaveChanges();
            return list;
        }

        public UserList LockUserList(int id)
        {
            UserList list = GetUserList(id);
            list.ListStatus = UserListStatus.Locked;
            Context.SaveChanges();
            return list;
        }

        public UserList OpenUserList(int id)
        {
            UserList list = GetUserList(id);
            list.ListStatus = UserListStatus.Open;
            Context.SaveChanges();
            return list;
        }
    }
}
