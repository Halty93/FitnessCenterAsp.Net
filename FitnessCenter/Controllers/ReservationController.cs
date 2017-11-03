using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Dao;
using DataAccess.Model;

namespace FitnessCenter.Controllers
{
    public class ReservationController : Controller
    {
        // GET: Reservation
        public ActionResult Index()
        {
            ReservationDao rDao = new ReservationDao();
            IList<Reservation> res= rDao.GetAll();
            return View(res);
        }

    }
}