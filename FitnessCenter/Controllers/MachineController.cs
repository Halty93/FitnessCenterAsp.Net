using System;
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
    public class MachineController : Controller
    {
        // GET: Machine
        public ActionResult Index()
        {
            MachineDao mDao = new MachineDao();

            ViewBag.Mark = "Machine";

            return View(mDao.GetAll());
        }
    }
}