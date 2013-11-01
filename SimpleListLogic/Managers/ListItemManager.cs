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
    }
}
