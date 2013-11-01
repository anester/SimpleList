using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleListContext;
using SimpleListData;
using SimpleListLogic.Web;

namespace SimpleListLogic.Mngr
{
    public class LoginManager
    {
        ListDbContext context { get; set; }
        ISimpleListSession session { get; set; }

        public LoginManager(ISimpleListSession session, string connectionstringname = "DefaultConnection")
        {
            context = new ListDbContext(connectionstringname);
        }

        public bool Login(string userid, string userpassword)
        {
            //TODO add password encryption
            return false;
        }

        public bool CreateLogin(User user)
        {
            return false;
        }

        
    }
}
