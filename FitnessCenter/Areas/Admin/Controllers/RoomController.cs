using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Dao;
using DataAccess.Model;

namespace FitnessCenter.Areas.Admin.Controllers
{
    [Authorize(Roles = "Ředitel")]
    public class RoomController : Controller
    {
        // GET: Admin/Room
        public ActionResult Index()
        {
            RoomDao rDao = new RoomDao();
            IList<Room> r = rDao.GetAll();

            ViewBag.Mark = "Room";

            return View(r);
        }

        public ActionResult Create()
        {
            ViewBag.Mark = "Room";
            return View();
        }

        [HttpPost]
        public ActionResult Add(Room room)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RoomDao rDao = new RoomDao();
                  
                    if (rDao.RoomExist(room.Name) == false)
                    {
                        rDao.Create(room);
                    }
                    else
                    {
                        TempData["warning"] = "Stroj pod tímto názvem již existuje!";
                        return View("Create", room);
                    }

                    TempData["succes"] = "Místnost úspěšně přidána.";
                }
                else
                {
                    return View("Create", room);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            RoomDao rDao = new RoomDao();
            ViewBag.Mark = "Room";

            return View(rDao.GetById(id));
        }

        //pridat upozorneni pro zmenu kapacity
        [HttpPost]
        public ActionResult Update(Room room)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RoomDao rDao = new RoomDao();
                    Room.CapacityCheck(room);
                    
                    rDao.Update(room);

                    TempData["succes"] = "Místnost úspěšně upravena.";
                }
                else
                {
                    return View("Edit", room);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return RedirectToAction("Index");
        }

        //pridat upozorneni pro vypsane teminy
        public ActionResult Delete(int id)
        {
            try
            {
                RoomDao rDao = new RoomDao();
                Room r = rDao.GetById(id);

                //je zapotřebí smazat nejdrive vsechny terminy v teto mistnosti, a take rezervace na tyto terminy
                TermDao tDao = new TermDao();
                ReservationDao resDao = new ReservationDao();
                IList<Term> terms = tDao.GetTermsByRoom(r);
                foreach (Term t in terms)
                {
                    IList<Reservation> reservations = resDao.GetAllReservationsByTerm(t);
                    foreach (Reservation res in reservations)
                    {
                        resDao.Delete(res);
                    }
                    tDao.Delete(t);
                }

                rDao.Delete(r);
                TempData["succes"] = "Místnost " + r.Name +
                                     " byla odstraněna, stejně tak i všechny termíny v této místnosti.";
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