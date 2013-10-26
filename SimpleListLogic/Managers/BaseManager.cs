using SimpleListContext;
using SimpleListLogic.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleListLogic.Managers
{
    public class BaseManager
    {
        public ListDbContext Context { get; protected set; }
        public ISimpleListSession Session { get; protected set; }

        public BaseManager(string connectionstring = "DefaultConnection")
        {
            Context = new ListDbContext(connectionstring);
        }

        public BaseManager(ISimpleListSession session, string connectionstring = "DefaultConnection")
            : this(connectionstring)
        {
            Session = session;
        }
    }
}
