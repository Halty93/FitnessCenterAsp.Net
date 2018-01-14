using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Dao;
using DataAccess.Model;
using FitnessCenter.Class;

namespace FitnessCenter.Controllers
{
    public class UserController : Controller
    {
        public ActionResult CreateUser()
        {
            ViewBag.Mark = "User";
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(FitnessUser fitnessUser, HttpPostedFileBase picture)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (picture != null)
                    {
                        ImageClass.ImageMethod(picture, "FitnessUser", out string bigImageName, out string smallImageName, out string tempData);

                        if (tempData != null)
                        {
                            TempData["warning"] = tempData;
                        }
                        fitnessUser.BigImageName = bigImageName;
                        fitnessUser.SmallImageName = smallImageName;
                    }
                    UserDao uDao = new UserDao();
                    AddressDao aDao = new AddressDao();
                    Address a = new Address();

                    fitnessUser.Role = new RoleDao().GetById(399);
                    fitnessUser.Password = PasswordHash.CreateHash(fitnessUser.Password);
                    a = fitnessUser.Address;

                    if (uDao.LoginExist(fitnessUser.Login) == false)
                    {
                        aDao.Create(a);
                        fitnessUser.Address = a;
                        uDao.Create(fitnessUser);                       
                    }
                    else
                    {
                        TempData["warning"] = "Uživatel pod tímto loginem již existuje!";
                        return View("CreateUser", fitnessUser);
                    }
                }
                else
                {
                    return View("CreateUser", fitnessUser);
                }
                if (TempData["warning"] == null)
                {
                    TempData["succes"] = "Registrace proběhla úspěšně.";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return RedirectToAction("Index", "Home");
        }
    }
}