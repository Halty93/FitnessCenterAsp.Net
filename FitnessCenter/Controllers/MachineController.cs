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
        public ActionResult Index(int? page, int? item)
        {
            int itemsOnPage = item ?? 5;
            int pg = page ?? 1;

            MachineDao mDao = new MachineDao();
            IList<Machine> machs = mDao.GetMachinePage(itemsOnPage, pg);

            ViewBag.Pages = (int)Math.Ceiling((double)mDao.GetAll().Count / (double)itemsOnPage);
            ViewBag.CurrentPage = pg;
            ViewBag.Items = itemsOnPage;
            ViewBag.Mark = "Machine";
            if (Request.IsAjaxRequest())
            {
                return PartialView(machs);
            }
            return View(machs);
        }
    }
}