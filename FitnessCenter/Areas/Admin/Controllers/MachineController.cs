﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Dao;
using DataAccess.Model;
using FitnessCenter.Class;

namespace FitnessCenter.Areas.Admin.Controllers
{
    [Authorize]
    public class MachineController : Controller
    {
        // GET: Admin/Machine
        public ActionResult Index(int? page, int? item)
        {
            int itemsOnPage = item ?? 5;
            int pg = page ?? 1;

            MachineDao mDao = new MachineDao();
            IList<Machine> machs = mDao.GetMachinePage(itemsOnPage, pg);
            UserDao uDao = new UserDao();
            FitnessUser u = uDao.GetByLogin(User.Identity.Name);

            ViewBag.Pages = (int)Math.Ceiling((double)mDao.GetAll().Count / (double)itemsOnPage);
            ViewBag.CurrentPage = pg;
            ViewBag.Items = itemsOnPage;
            ViewBag.Mark = "Machine";

            if (Request.IsAjaxRequest())
            {
                if (u.Role.Name != "Údržbář")
                {
                    return PartialView("CustomerIndex", machs);
                }
                return PartialView(machs);
            }
            else
            {
                if (u.Role.Name != "Údržbář")
                {
                    return View("CustomerIndex", machs);
                }
                return View(machs);
            }
        }

        [Authorize(Roles = "Údržbář")]
        public ActionResult Detail(int id)
        {
            MachineDao mDao = new MachineDao();
            Machine mach = mDao.GetById(id);
            ViewBag.Mark = "Machine";
            if (Request.IsAjaxRequest())
            {
                return PartialView(mach);
            }
            return View(mach);
        }

        public ActionResult Status(string status, int? page, int? item)
        {
            int itemsOnPage = item ?? 5;
            int pg = page ?? 1;

            MachineDao mDao = new MachineDao();
            IList<Machine> machs = mDao.GetMachinesByStatus(itemsOnPage, pg, status);

            ViewBag.Pages = (int)Math.Ceiling((double)machs.Count / (double)itemsOnPage);
            ViewBag.CurrentPage = pg;
            ViewBag.Items = itemsOnPage;

            ViewBag.Roles = new RoleDao().GetAll();
            ViewBag.CurrentStatus = status;
            ViewBag.Mark = "Machine";

            if (Request.IsAjaxRequest())
            {
                return PartialView("Index", machs);
            }
            return View("Index", machs);
        }

        [Authorize(Roles = "Údržbář")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Machine mach, HttpPostedFileBase picture, string status)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (picture != null)
                    {
                        ImageClass.ImageMethod(picture, "Machine", out string bigImageName, out string smallImageName, out string tempData);

                        if (tempData != null)
                        {
                            TempData["warning"] = tempData;
                        }
                        mach.BigImageName = bigImageName;
                        mach.SmallImageName = smallImageName;
                    }
                    MachineDao mDao = new MachineDao();

                    bool isExist = mDao.MachineExist(mach.Name);
                    if (isExist == false)
                    {
                        UserDao uDao = new UserDao();
                        mach.Repairman = uDao.GetByLogin(User.Identity.Name);

                        mDao.Create(mach);
                    }
                    else
                    {
                        TempData["warning"] = "Stroj pod tímto názvem již existuje!";
                        return View("Create", mach);
                    }
                }
                else
                {
                    return View("Create", mach);
                }
                if (TempData["warning"] == null)
                {
                    TempData["succes"] = "Nový stroj " + mach.Name + " úspěšně přidán.";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return RedirectToAction("Index");
        }

        public ActionResult EditFault(int id)
        {
            MachineDao mDao = new MachineDao();
            ViewBag.Mark = "Machine";

            return View(mDao.GetById(id));
        }

        [HttpPost]
        public ActionResult UpdateFault(Machine m, int repairmanId)
        {
            try
            {
                if (m.Fault != null)
                {
                    MachineDao mDao = new MachineDao();
                    UserDao uDao = new UserDao();

                    m.FaultUser = uDao.GetByLogin(User.Identity.Name);
                    m.Repairman = uDao.GetById(repairmanId);
                    m.FaultDate = DateTime.Today;
                    m.Status = "Poškozený";

                    mDao.Update(m);

                    TempData["succes"] = "Závada na stroji " + m.Name + " přidána.";
                }
                else
                {
                    TempData["warning"] = "Nebyl vyplněn popis závady.";
                    return View("EditFault", m);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Údržbář")]
        public ActionResult Edit(int id)
        {
            MachineDao mDao = new MachineDao();
            ViewBag.Stats = Machine.StatsList;
            ViewBag.Mark = "Machine";

            return View(mDao.GetById(id));
        }

        [HttpPost]
        public ActionResult Update(Machine mach, HttpPostedFileBase picture, string status)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (picture != null)
                    {
                        ImageClass.ImageMethod(picture, "Machine", out string bigImageName, out string smallImageName, out string tempData);

                        if (tempData != null)
                        {
                            TempData["warning"] = tempData;
                        }

                        if (mach.SmallImageName != null)
                        {
                            System.IO.File.Delete(Server.MapPath("~/Uploads/Machine/" + mach.SmallImageName));
                        }
                        if (mach.BigImageName != null)
                        {
                            System.IO.File.Delete(Server.MapPath("~/Uploads/Machine/" + mach.BigImageName));
                        }

                        mach.BigImageName = bigImageName;
                        mach.SmallImageName = smallImageName;
                    }
                    MachineDao mDao = new MachineDao();
                    UserDao uDao = new UserDao();

                    mach.Repairman = uDao.GetByLogin(User.Identity.Name);
                    mach.Status = status;

                    if (mach.Status == "Poškozený")
                    {
                        mach.FaultUser = uDao.GetByLogin(User.Identity.Name);
                        mach.FaultDate = DateTime.Today;
                    }

                    mDao.Update(mach);
                }
                else
                {
                    return View("Edit", mach);
                }
                if (TempData["warning"] == null)
                {
                    TempData["succes"] = "Úprava stroje " + mach.Name + " proběhla úspěšně.";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Údržbář")]
        public ActionResult Delete(int id)
        {
            try
            {
                MachineDao mDao = new MachineDao();
                Machine m = mDao.GetById(id);

                if (m.SmallImageName != null)
                {
                    System.IO.File.Delete(Server.MapPath("~/Uploads/Machine/" + m.SmallImageName));
                }
                if (m.BigImageName != null)
                {
                    System.IO.File.Delete(Server.MapPath("~/Uploads/Machine/" + m.BigImageName));
                }

                mDao.Delete(m);

                TempData["succes"] = "Stroj " + m.Name + " úspěšně smazán.";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Údržbář")]
        public ActionResult DeletePicture(int id)
        {
            try
            {
                MachineDao mDao = new MachineDao();
                Machine m = mDao.GetById(id);

                if (m.SmallImageName != null)
                {
                    System.IO.File.Delete(Server.MapPath("~/Uploads/Machine/" + m.SmallImageName));
                }
                if (m.BigImageName != null)
                {
                    System.IO.File.Delete(Server.MapPath("~/Uploads/Machine/" + m.BigImageName));
                }

                m.SmallImageName = null;
                m.BigImageName = null;

                TempData["succes"] = "Obrázek stroje odstraněn.";

                return View("Edit", m);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}