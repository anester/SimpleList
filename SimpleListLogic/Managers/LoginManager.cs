using SimpleListLogic.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SimpleListLogic.Managers
{
    public enum UserCreateToken
    {
        Success,
        UserNameInvalid,
        UserNameAlreadyExists
    }

    public class LoginManager : BaseManager
    {
        public LoginManager(ISimpleListSession session)
            : base(session)
        {
        }

        public bool IsLoggedIn()
        {
            return Session.CurrentLogin != null;
        }

        public bool LogIn(string loginname, string password)
        {
            var login = Context.Logins.FirstOrDefault(l => l.LoginName == loginname && l.Password == password);
            if (login != null)
            {
                Session.CurrentLogin = login;
                Session.LoggedInTime = DateTime.Now;
            }
            return IsLoggedIn();
        }

        public UserCreateToken CreateLogin(string loginname, string password, string firstname, string lastname, string email)
        {
            SimpleListData.Login newlogin = new SimpleListData.Login()
            {
                LoginName = loginname,
                Password = password
            };

            if (Context.Logins.Count(l => l.LoginName == loginname) > 0)
            {
                return UserCreateToken.UserNameAlreadyExists;
            }

            Context.Logins.Add(newlogin);
            Context.SaveChanges();

            SimpleListData.User newuser = new SimpleListData.User()
            {
                FirstName = firstname,
                LastName = lastname,
                EmailAddress = email,
                LoginId = newlogin.LoginId
            };

            Context.Users.Add(newuser);
            Context.SaveChanges();
            return UserCreateToken.Success;
        }
    }
}
