using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Dao;
using DataAccess.Model;

namespace FitnessCenter.Controllers
{
    public class TermController : Controller
    {
        // GET: Term
        public ActionResult Index()
        {
            TermDao tDao = new TermDao();
            IList<Term> terms = tDao.GetAll();
            return View(terms);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Term term)
        {
            if (ModelState.IsValid)
            {
                //přidat termín
            }
            else
            {
                return View("Create", term);
            }

            return RedirectToAction("Index");
        }
    }
}