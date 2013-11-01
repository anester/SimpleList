using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq;

namespace SimpleList
{

    public class SimpleListSession : SimpleListLogic.Web.ISimpleListSession
    {
        public const string CURRENTLOGINKEY = "CURRENTLOGIN";
        public const string CURRENTUSERKEY = "CURRENTUSER";
        public const string LOGGEDINTIMEKEY = "TIMELOGGEDIN";

        private System.Web.SessionState.HttpSessionState Session
        {
            get
            {
                return HttpContext.Current.Session;
            }
        }

        private T get<T>(string key)
        {
            if (Session[key] == null)
            {
                return default(T);
            }
            return (T)Session[key];
        }

        private void set<T>(string key, T value)
        {
            if (Session[key] == null)
            {
                Session.Add(key, value);
            }
            else
            {
                Session[key] = value;
            }
        }

        public string CurrentLoginName
        {
            get { return CurrentLogin == null ? "" : CurrentLogin.LoginName; }
            set { }
        }

        public SimpleListData.Login CurrentLogin
        {
            get { return get<SimpleListData.Login>(CURRENTLOGINKEY); }
            set { set(CURRENTLOGINKEY, value); }
        }

        public SimpleListData.User LoggedInUser
        {
            get { return get<SimpleListData.User>(CURRENTUSERKEY); }
            set { set(CURRENTUSERKEY, value); }
        }

        public DateTime LoggedInTime
        {
            get { return get<DateTime>(LOGGEDINTIMEKEY); }
            set { set(LOGGEDINTIMEKEY, value); }
        }
    }
}