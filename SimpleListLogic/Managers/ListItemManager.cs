using SimpleListData;
using SimpleListLogic.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleListLogic.Managers
{
    public class ListItemManager : BaseManager
    {
        public ListItemManager(ISimpleListSession session)
            : base(session)
        {
        }

        public IEnumerable<ListItem> GetItems(int userlistid)
        {
            var items = Context.ListItems.Where(i => i.UserListId == userlistid);
            return items;
        }

        public bool DeleteItem(int listitemid)
        {
            ListItem listitem = Context.ListItems.Find(listitemid);
            if (listitem == null)
            {
                return false;
            }
            Context.ListItems.Remove(listitem);
            Context.SaveChanges();
            return true;
        }

        public bool MarkAsDone(int id)
        {
            ListItem item = Context.ListItems.Find(id);

            if (item == null)
            {
                return false;
            }
            
            item.DateDone = DateTime.Now;
            Context.SaveChanges();

            return true;
        }

        public IEnumerable<ListItem> GetItemsFromListItemId(int id)
        {
            int listid = Context.ListItems.Where(i => i.ListItemId == id)
                                          .Select(i => i.UserListId)
                                          .FirstOrDefault();

            return Context.ListItems.Where(i => i.UserListId == listid);
        }
    }
}
