using SimpleListData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleListContext
{
    public class ListDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserList> UserLists { get; set; }
        public DbSet<ListItem> ListItems { get; set; }
    }
}
