using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Dao;
using DataAccess.Model;

namespace FitnessCenter.Controllers
{
    public class RoomController : Controller
    {
        // GET: Room
        public ActionResult Index()
        {
            RoomDao rDao = new RoomDao();
            IList<Room> r = rDao.GetAll();
            
            return View(r);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Room room)
        {
            if (ModelState.IsValid)
            {
                //pridat místnost
            }
            else
            {
                return View("Create", room);
            }

            return RedirectToAction("Index");
        }
    }
}