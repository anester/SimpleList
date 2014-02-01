using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleListData
{
    public class ItemPart
    {
        public int ListItemId { get; set; }
        public int ItemPartId { get; set; }
        public string Description { get; set; }
        public int OrderNum { get; set; }

        public virtual ListItem ParentItem { get; set; }
    }
}
