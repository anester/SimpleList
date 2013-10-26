using SimpleListData;
using SimpleListLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleList.Controllers
{
    public class LoginController : Controller
    {
        LoginManager Manager { get; set; }
        SimpleListSession SimpleSession { get; set; }

        public LoginController()
        {
            SimpleSession = new SimpleListSession();
            Manager = new LoginManager(SimpleSession);
        }
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Login login)
        {
            if (Manager.LogIn(login.LoginName, login.Password))
            {
                return Redirect("/List/Index/" + login.LoginName);
            }
            else
            {
                return RedirectToAction("Create");
            }
            return View();
        }


        //
        // GET: /Login/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Login/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Login/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                var res = Manager.CreateLogin(user.UserLogin.LoginName, user.UserLogin.Password, user.FirstName, user.LastName, user.EmailAddress);

                if (res == UserCreateToken.Success)
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Create");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Login/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Login/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Login/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Login/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
