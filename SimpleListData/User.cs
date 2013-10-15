using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleListData
{
    public class User
    {
        public int UserId { get; set; }
        public string UserLogin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }

        public virtual IEnumerable<UserList> UserLists { get; set; }
    }
}
