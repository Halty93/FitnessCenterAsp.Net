using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DataAccess.Dao;
using DataAccess.Model;
using FitnessCenter.Class;

namespace FitnessCenter.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        //vyuzit sekce a modely
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(string login, string password)
        {
            UserDao uDao = new UserDao();
            FitnessUser u = uDao.GetByLogin(login);
            //if (u != null && PasswordHash.ValidatePassword(password, u.Password))
            //{
            //    password = u.Password;
            //}
            //else
            //{
            //    TempData["error"] = "Přihlašovací údaje nejsou správné";
            //    return RedirectToAction("Index");
            //}

            if (Membership.ValidateUser(login, password))
            {
                FormsAuthentication.SetAuthCookie(login, false);

                TempData["success"] = "Přihlašení proběhlo správně";

                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }


            TempData["error"] = "Přihlašovací údaje nejsou správné";
            return RedirectToAction("Index");
        }
    }
}