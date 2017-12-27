using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            IList<Term> terms = tDao.GetNewTerms();

            ViewBag.Mark = "Term";

            return View(terms);
        }
    }
}