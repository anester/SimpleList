using SimpleListData;
using SimpleListLogic.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var lists = Context.UserLists.Where(ul => ul.Owner.UserLogin.LoginName == lookupname).ToList();
            foreach (var list in lists)
            {
                list.ListItems = Context.ListItems.Where(i => i.UserListId == list.UserListId);
            }
            return lists;
        }

        public UserList CreateUserList(string username, string listname, string description = "")
        {
            int id = Context.Logins.Where(l => l.LoginName == username).Select(l => l.LoginId).FirstOrDefault();
            UserList list = new UserList
            {
                UserId = id,
                UserListName = listname,
                Description = description
            };

            Context.UserLists.Add(list);
            Context.SaveChanges();

            return list;
        }

        public UserList GetUserList(int id)
        {
            return Context.UserLists.FirstOrDefault(l => l.UserListId == id);
        }
    }
}
