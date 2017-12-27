using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Dao;
using DataAccess.Model;
using System.Globalization;

namespace FitnessCenter.Areas.Admin.Controllers
{
    [Authorize]
    public class TermController : Controller
    {
        // GET: Admin/Term
        public ActionResult Index(int? currentTerms)
        {
            int curT = currentTerms ?? 1;

            TermDao tDao = new TermDao();
            IList<Term> terms = null;
            UserDao uDao = new UserDao();
            FitnessUser u = uDao.GetByLogin(User.Identity.Name);

            if (u.Role.Name == "Trenér" && curT == 0)
            {
                terms = tDao.GetOldTermsByTrainer(u);
            }
            else if (u.Role.Name == "Trenér" && curT == 1)
            {
                terms = tDao.GetNewTermsByTrainer(u);
            }
            else if (u.Role.Name == "Ředitel" && curT == 0)
            {
                terms = tDao.GetOldTerms();
            }
            else // pro zakaznazniky i pro if (u.Role.Name == "Ředitel" && curT == 1) plati ze se zobrazi vsechny aktualni terminy
            {
                terms = tDao.GetNewTerms();
            }

            ViewBag.CurrentTerm = curT;
            ViewBag.Mark = "Term";

            if (u.Role.Name == "Zákazník")
            {
                return View("CustomerIndex", terms);
            }
            return View(terms);
        }

        [Authorize(Roles = "Trenér")]
        public ActionResult Create()
        {
            ViewBag.Rooms = new RoomDao().GetAll();
            ViewBag.Activities = new ActivityDao().GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult Add(Term term, int activityId, int roomId, int timeLong)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TermDao tDao = new TermDao();
                    ActivityDao aDao = new ActivityDao();
                    RoomDao rDao = new RoomDao();
                    UserDao uDao = new UserDao();

                    DateTime end = term.StartTerm.AddMinutes((double)timeLong);


                    term.EndTerm = end;
                    term.Activity = aDao.GetById(activityId);
                    term.Room = rDao.GetById(roomId);
                    term.Trainer = uDao.GetByLogin(User.Identity.Name);

                    ViewBag.Rooms = new RoomDao().GetAll();
                    ViewBag.Activities = new ActivityDao().GetAll();

                    //kontrola kapacity terminu
                    if (term.Room.MaxCapacity > term.Capacity)
                    {
                        IList<Term> termsInRoom = tDao.GetTermsByRoom(term.Room);
                        bool validDates = true;
                        //kontrola dostupnosti mistnosti ve zvoleny cas
                        foreach (Term t in termsInRoom)
                        {
                            if (
                                !( //zapor
                                    (t.EndTerm > DateTime.Now) && //neukoncene terminy
                                    (
                                      (t.StartTerm > term.StartTerm && t.StartTerm >= term.EndTerm) || //zacatek i konec terminu je pred zacatkem jineho terminu
                                      (t.EndTerm <= term.StartTerm && t.EndTerm < term.EndTerm)  //zacatek i konec terminu je po konci jineho terminu
                                    )
                                )
                              )
                            {
                                validDates = false;
                                break;
                            }
                        }
                        if (validDates == true)
                        {
                            tDao.Create(term);
                            TempData["succes"] = "Termín úspěšně vytvořen.";
                        }
                        else
                        {
                            TempData["warning"] = "Časový plán termínu je v konfilktu s jiným termínem ve stejné místnosti.";
                            return View("Create", term);
                        }
                    }
                    else
                    {
                        TempData["warning"] = "Maximální kapacita místnosti je menší než kapacita vytvářeného termínu.";
                        return View("Create", term);
                    }
                }
                else
                {
                    return View("Create", term);
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
            TermDao tDao = new TermDao();
            ViewBag.Rooms = new RoomDao().GetAll();
            ViewBag.Activities = new ActivityDao().GetAll();
            ViewBag.Start = tDao.GetById(id).StartTerm;
            ViewBag.TimeLong = (int)(tDao.GetById(id).EndTerm.Subtract(tDao.GetById(id).StartTerm).Minutes);
            return View(tDao.GetById(id));
        }

        [HttpPost]
        public ActionResult Update(Term term, int activityId, int roomId, int timeLong)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TermDao tDao = new TermDao();
                    ActivityDao aDao = new ActivityDao();
                    RoomDao rDao = new RoomDao();
                    UserDao uDao = new UserDao();

                    DateTime end = term.StartTerm.AddMinutes((double)timeLong);

                    term.EndTerm = end;
                    term.Activity = aDao.GetById(activityId);
                    term.Room = rDao.GetById(roomId);
                    term.Trainer = uDao.GetByLogin(User.Identity.Name);
                    if (term.Room.MaxCapacity > term.Capacity)
                    {
                        IList<Term> termsInRoom = tDao.GetTermsByRoom(term.Room);
                        bool validDates = true;
                        //kontrola dostupnosti mistnosti ve zvoleny cas
                        foreach (Term t in termsInRoom)
                        {
                            if (
                                !( //zapor
                                    (t.StartTerm > term.StartTerm && t.StartTerm >= term.EndTerm) || //zacatek i konec terminu je pred zacatkem jineho terminu
                                    (t.EndTerm <= term.StartTerm && t.EndTerm < term.EndTerm)  //zacatek i konec terminu je po konci jineho terminu
                                )
                              )
                            {
                                validDates = false;
                                break;
                            }
                        }
                        if (validDates == true)
                        {
                            tDao.Update(term);
                            TempData["succes"] = "Termín úspěšně upraven.";
                        }
                        else
                        {
                            TempData["warning"] = "Časový plán termínu je v konfilktu s jiným termínem ve stejné místnosti.";
                            return View("Create", term);
                        }
                    }
                    else
                    {
                        TempData["warning"] = "Maximální kapacita místnosti je menší než upravená kapacita termínu.";
                        return View("Edit", term);
                    }
                }
                else
                {
                    return View("Edit", term);
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
                TermDao tDao = new TermDao();
                Term t = tDao.GetById(id);

                //je treba smazat i vsechny rezervace daneho terminu
                ReservationDao resDao = new ReservationDao();
                IList<Reservation> reservations = resDao.GetAllReservationsByTerm(t);
                foreach (Reservation res in reservations)
                {
                    resDao.Delete(res);
                }

                tDao.Delete(t);
                TempData["succes"] = "Termín i jeho rezervace jsou úspěšně smazány.";
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