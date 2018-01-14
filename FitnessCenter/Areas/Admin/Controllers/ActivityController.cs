using System;
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
    public class ActivityController : Controller
    {
        // GET: Admin/Activity
        public ActionResult Index()
        {
            ActivityDao ad = new ActivityDao();
            IList<Activity> acts = ad.GetAll();
            UserDao uDao = new UserDao();
            FitnessUser u = uDao.GetByLogin(User.Identity.Name);

            ViewBag.Mark = "Activity";

            if (u.Role.Name != "Trenér")
            {
                return View("CustomerIndex", acts);
            }

            return View(acts);
        }

        [Authorize(Roles = "Trenér")]
        public ActionResult Create()
        {
            ViewBag.Mark = "Activity";
            return View();
        }

        [HttpPost]
        public ActionResult Add(Activity act, HttpPostedFileBase picture)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (picture != null)
                    {
                        ImageClass.ImageMethod(picture, "Activity", out string bigImageName, out string smallImageName, out string tempData);

                        if (tempData != null)
                        {
                            TempData["warning"] = tempData;
                        }
                        act.BigImageName = bigImageName;
                        act.SmallImageName = smallImageName;
                    }

                    ActivityDao aDao = new ActivityDao();
                    bool isExist = aDao.ActivityExist(act.Name);
                    if (isExist == false)
                    {
                        UserDao uDao = new UserDao();
                        act.Author = uDao.GetByLogin(User.Identity.Name);

                        aDao.Create(act);
                    }
                    else
                    {
                        TempData["warning"] = "Aktivita pod tímto názvem již existuje!";
                        return View("Create", act);
                    }
                }
                else
                {
                    return View("Create", act);
                }

                if (TempData["warning"] == null)
                {
                    TempData["succes"] = "Nová aktivita " + act.Name + " úspěšně přidána.";
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Trenér")]
        public ActionResult Edit(int id)
        {
            ViewBag.Mark = "Activity";
            ActivityDao aDao = new ActivityDao();
            return View(aDao.GetById(id));
        }

        [HttpPost]
        public ActionResult Update(Activity act, HttpPostedFileBase picture, int authorId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (picture != null)
                    {
                        ImageClass.ImageMethod(picture, "Activity", out string bigImageName, out string smallImageName, out string tempData);

                        if (tempData != null)
                        {
                            TempData["warning"] = tempData;
                        }

                        System.IO.File.Delete(Server.MapPath("~/Uploads/Activity/" + act.SmallImageName));
                        System.IO.File.Delete(Server.MapPath("~/Uploads/Activity/" + act.BigImageName));

                        act.BigImageName = bigImageName;
                        act.SmallImageName = smallImageName;
                    }

                    ActivityDao aDao = new ActivityDao();
                    UserDao userDao = new UserDao();
                    act.Author = userDao.GetById(authorId);
                    aDao.Update(act);
                }
                else
                {
                    return View("Edit", act);
                }

                if (TempData["warning"] == null)
                {
                    TempData["succes"] = "Úprava aktivity " + act.Name + " proběhla úspěšně.";
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Trenér")]
        public ActionResult Delete(int id)
        {
            try
            {
                ActivityDao aDao = new ActivityDao();
                Activity a = aDao.GetById(id);

                System.IO.File.Delete(Server.MapPath("~/Uploads/Activity/" + a.SmallImageName));
                System.IO.File.Delete(Server.MapPath("~/Uploads/Activity/" + a.BigImageName));

                //je zapotřebí smazat nejdrive vsechny terminy na tuto aktivitu, a take rezervace na tyto terminy
                TermDao tDao = new TermDao();
                ReservationDao resDao = new ReservationDao();
                IList<Term> terms = tDao.GetTermsByActivity(a);
                foreach (Term t in terms)
                {
                    IList<Reservation> reservations = resDao.GetAllReservationsByTerm(t);
                    foreach (Reservation res in reservations)
                    {
                        resDao.Delete(res);
                    }
                    tDao.Delete(t);
                }

                aDao.Delete(a);

                TempData["succes"] = "Aktivita " + a.Name + " úspěšně smazána. Stejně tak i všechny termíny na tuto akci.";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Trenér")]
        public ActionResult DeletePicture(int id)
        {
            try
            {
                ActivityDao aDao = new ActivityDao();
                Activity a = aDao.GetById(id);

                System.IO.File.Delete(Server.MapPath("~/Uploads/Activity/" + a.SmallImageName));
                System.IO.File.Delete(Server.MapPath("~/Uploads/Activity/" + a.BigImageName));

                a.SmallImageName = null;
                a.BigImageName = null;

                TempData["succes"] = "Obrázek aktivity odstraněn.";

                return View("Edit", a);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}