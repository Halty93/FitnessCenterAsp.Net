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
        // GET: FitnessUser
        //public ActionResult Index()
        //{
        //    UserDao uDao = new UserDao();
        //    IList<FitnessUser> users = uDao.GetAll();
        //    return View(users);
        //}

        public ActionResult CreateUser()
        {
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
                    a.Country = fitnessUser.Address.Country;
                    a.Street = fitnessUser.Address.Street;
                    a.StreetNumber = fitnessUser.Address.StreetNumber;
                    a.Town = fitnessUser.Address.Town;
                    a.Zip = fitnessUser.Address.Zip;

                    uDao.Create(fitnessUser);
                    aDao.Create(a);
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