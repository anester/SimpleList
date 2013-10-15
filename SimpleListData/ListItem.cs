using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleListData
{
    public class ListItem
    {
        public int ListItemId { get; set; }
        public int UserListId { get; set; }
        public string ListItemName { get; set; }
        public string Description { get; set; }
        public DateTime DateEntered { get; set; }
        public DateTime? DateDone { get; set; }

        public virtual UserList List { get; set; }
    }
}
