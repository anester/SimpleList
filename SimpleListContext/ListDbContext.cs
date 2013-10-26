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
        public ListDbContext()
            : this("DefaultConnection")
        {

        }

        public ListDbContext(string constring)
            : base(constring)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserList> UserLists { get; set; }
        public DbSet<ListItem> ListItems { get; set; }
        public DbSet<Login> Logins { get; set; }
    }
}
