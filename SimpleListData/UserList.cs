using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleListData
{
    public class UserList
    {
        public int UserListId { get; set; }
        public int UserId { get; set; }
        public string UserListName { get; set; }
        public string Description { get; set; }

        public virtual IEnumerable<ListItem> ListItems { get; set; }
        public virtual User Owner { get; set; }
    }
}
