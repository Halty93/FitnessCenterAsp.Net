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
    public class ActivityController : Controller
    {
        // GET: Activity
        public ActionResult Index()
        {
            ActivityDao ad = new ActivityDao();
            IList<Activity> acts = ad.GetAll();

            ViewBag.Mark = "Activity";
            return View(acts);
        }

    }
}