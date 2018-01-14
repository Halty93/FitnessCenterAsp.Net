using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Dao;
using DataAccess.Model;

namespace FitnessCenter.Areas.Admin.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        // GET: Admin/Reservation
        public ActionResult Index(int? page, int? item)
        {
            int itemsOnPage = item ?? 5;
            int pg = page ?? 1;

            UserDao uDao = new UserDao();
            FitnessUser u = uDao.GetByLogin(User.Identity.Name);
            ReservationDao rDao = new ReservationDao();
            IList<Reservation> res = null;
            if (u.Role.Name == "Zákazník")
            {
                res = rDao.GetReservationPageByUser(itemsOnPage, pg, u);
            }
            else
            {
                res = rDao.GetReservationPage(itemsOnPage, pg);
            }

            ViewBag.Pages = (int)Math.Ceiling((double)rDao.GetAll().Count / (double)itemsOnPage);
            ViewBag.CurrentPage = pg;
            ViewBag.Items = itemsOnPage;
            ViewBag.Mark = "Reservation";

            if (Request.IsAjaxRequest())
            {
                return PartialView(res);
            }
            return View(res);
        }

        //rezervace se bude zadavat v planu terminu
        //[HttpPost]
        public ActionResult Add(int termId)
        {
            try
            {
                Reservation r = new Reservation();
                ReservationDao rDao = new ReservationDao();
                TermDao tDao = new TermDao();
                UserDao uDao = new UserDao();

                r.Term = tDao.GetById(termId);
                r.User = uDao.GetByLogin(User.Identity.Name);
                r.ReservationTime = DateTime.Now;

                if (tDao.GetActualCapacity(r.Term)>0 && rDao.ReservationExist(r.Term, r.User) == false)
                {
                    rDao.Create(r);
                    TempData["succes"] = "Termín úspěšně rezervován.";
                }
                else
                {
                    TempData["warning"] = "Termín je již plně obsazen.";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return RedirectToAction("Index", "Term");
        }

        public ActionResult Delete(int id)
        {
            try
            {
                ReservationDao rDao = new ReservationDao();

                rDao.Delete(rDao.GetById(id));
                TempData["succes"] = "Rezervace na termín úspěšně zrušena.";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return RedirectToAction("Index");
        }
    }
}