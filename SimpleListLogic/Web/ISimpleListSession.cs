using SimpleListData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleListLogic.Web
{
    public interface ISimpleListSession
    {
        string CurrentLoginName { get; set; }
        Login CurrentLogin { get; set; }
        User LoggedInUser { get; set; }
        DateTime LoggedInTime { get; set; }
    }
}
