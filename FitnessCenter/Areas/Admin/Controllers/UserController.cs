using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using DataAccess.Dao;
using DataAccess.Model;
using FitnessCenter.Class;

namespace FitnessCenter.Areas.Admin.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        // GET: Admin/FitnessUser
        [Authorize(Roles = "Ředitel")]
        public ActionResult Index(int? page, int? item)
        {
            int itemsOnPage = item ?? 1;
            int pg = page ?? 1;

            UserDao uDao = new UserDao();
            IList<FitnessUser> users = uDao.GetUserPage(itemsOnPage, pg);

            ViewBag.Pages = (int)Math.Ceiling((double)users.Count / (double)itemsOnPage);
            ViewBag.CurrentPage = pg;
            ViewBag.Items = itemsOnPage;
            ViewBag.Mark = "User";

            return View(users);
        }

        public ActionResult Search(string phrase, int? page, int? item, int? roleId)
        {
            int itemsOnPage = item ?? 5;
            int pg = page ?? 1;

            UserDao uDao = new UserDao();
            IList<FitnessUser> users = uDao.SearchUsers(phrase, itemsOnPage, pg, roleId);

            ViewBag.Pages = (int)Math.Ceiling((double)users.Count / (double)itemsOnPage);
            ViewBag.CurrentPage = pg;
            ViewBag.Items = itemsOnPage;
            ViewBag.Phrase = phrase;
            ViewBag.Mark = "User";
            // ViewBag.Categories = new UserCategoryDao().GetAll();

            return View("Index", users);
        }

        public ActionResult Role(int id, int? page, int? item)
        {
            int itemsOnPage = item ?? 5;
            int pg = page ?? 1;

            UserDao uDao = new UserDao();
            IList<FitnessUser> users = uDao.GetUsersByRole(itemsOnPage, pg, id);
            ViewBag.Pages = (int)Math.Ceiling((double)users.Count / (double)itemsOnPage);
            ViewBag.CurrentPage = pg;
            ViewBag.Items = itemsOnPage;

            ViewBag.Mark = "User";
            ViewBag.Roles = new RoleDao().GetAll();
            ViewBag.CurrentRole = id;
            //if (Request.IsAjaxRequest())
            //{
            //    return PartialView("Index", users);
            //}
            return View("Index", users);
        }

        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(FitnessUser fitnessUser, HttpPostedFileBase picture)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (picture != null)
                    {
                        ImageClass.ImageMethod(picture, "FitnessUser", out string bigImageName, out string smallImageName, out string tempData);

                        if (tempData != null)
                        {
                            TempData["warning"] = tempData;
                        }
                        fitnessUser.BigImageName = bigImageName;
                        fitnessUser.SmallImageName = smallImageName;
                    }
                    UserDao uDao = new UserDao();
                    AddressDao aDao = new AddressDao();
                    Address a = new Address();

                    fitnessUser.Role = new RoleDao().GetById(399);
                    a.Country = fitnessUser.Address.Country;
                    a.Street = fitnessUser.Address.Street;
                    a.StreetNumber = fitnessUser.Address.StreetNumber;
                    a.Town = fitnessUser.Address.Town;
                    a.Zip = fitnessUser.Address.Zip;

                    uDao.Create(fitnessUser);
                    aDao.Create(a);
                }
                else
                {
                    return View("CreateUser", fitnessUser);
                }
                if (TempData["warning"] == null)
                {
                    TempData["succes"] = "Registrace proběhla úspěšně.";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Ředitel")]
        public ActionResult CreateEmployee()
        {
            RoleDao rDao = new RoleDao();
            IList<Role> roles = new List<Role>();
            roles.Add(rDao.GetById(212));
            roles.Add(rDao.GetById(222));
            ViewBag.Roles = roles;

            return View();
        }


        [HttpPost]
        public ActionResult AddEmployee(FitnessUser fitnessUser, HttpPostedFileBase picture, int roleId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (picture != null)
                    {
                        ImageClass.ImageMethod(picture, "FitnessUser", out string bigImageName, out string smallImageName, out string tempData);

                        if (tempData != null)
                        {
                            TempData["warning"] = tempData;
                        }
                        fitnessUser.BigImageName = bigImageName;
                        fitnessUser.SmallImageName = smallImageName;
                    }
                    UserDao uDao = new UserDao();
                    AddressDao aDao = new AddressDao();
                    Address a = new Address();

                    fitnessUser.Role = new RoleDao().GetById(roleId);
                    fitnessUser.Password = PasswordHash.CreateHash(fitnessUser.Password);
                    a.Country = fitnessUser.Address.Country;
                    a.Street = fitnessUser.Address.Street;
                    a.StreetNumber = fitnessUser.Address.StreetNumber;
                    a.Town = fitnessUser.Address.Town;
                    a.Zip = fitnessUser.Address.Zip;

                    uDao.Create(fitnessUser);
                    aDao.Create(a);
                }
                else
                {
                    return View("CreateEmployee", fitnessUser);
                }
                if (TempData["warning"] == null)
                {
                    TempData["succes"] = "Založení zaměstnance proběhlo úspěšně.";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return RedirectToAction("Index");
        }

        public ActionResult EditAddress(int id)
        {
            UserDao uDao = new UserDao();
            return View(uDao.GetById(id));
        }

        [HttpPost]
        public ActionResult UpdateAddress(FitnessUser fitnessUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserDao uDao = new UserDao();
                    AddressDao aDao = new AddressDao();
                    Address a = new Address();

                    a.Id = fitnessUser.Address.Id;
                    a.Country = fitnessUser.Address.Country;
                    a.Street = fitnessUser.Address.Street;
                    a.StreetNumber = fitnessUser.Address.StreetNumber;
                    a.Town = fitnessUser.Address.Town;
                    a.Zip = fitnessUser.Address.Zip;

                    uDao.Update(fitnessUser);
                    aDao.Update(a);

                    TempData["succes"] = "Úprava adresy proběhlo úspěšně.";
                }
                else
                {
                    return View("EditAddress", fitnessUser);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult EditUser(int id)
        {
            UserDao uDao = new UserDao();
            return View(uDao.GetById(id));
        }

        [HttpPost]
        public ActionResult UpdateUser(FitnessUser fitnessUser, HttpPostedFileBase picture)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (picture != null)
                    {
                        ImageClass.ImageMethod(picture, "FitnessUser", out string bigImageName, out string smallImageName, out string tempData);

                        if (tempData != null)
                        {
                            TempData["warning"] = tempData;
                        }

                        System.IO.File.Delete(Server.MapPath("~/Uploads/FitnessUser/" + fitnessUser.SmallImageName));
                        System.IO.File.Delete(Server.MapPath("~/Uploads/FitnessUser/" + fitnessUser.BigImageName));

                        fitnessUser.BigImageName = bigImageName;
                        fitnessUser.SmallImageName = smallImageName;
                    }
                    UserDao uDao = new UserDao();

                    fitnessUser.Role = new RoleDao().GetById(399);

                    uDao.Update(fitnessUser);
                }
                else
                {
                    return View("EditUser", fitnessUser);
                }
                if (TempData["warning"] == null)
                {
                    TempData["succes"] = "Úprava Vašeho profilu proběhla úspěšně.";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Ředitel")]
        public ActionResult EditEmployee(int id)
        {
            UserDao uDao = new UserDao();
            return View(uDao.GetById(id));
        }

        public ActionResult UpdateEmployee(FitnessUser fitnessUser, HttpPostedFileBase picture, int roleId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (picture != null)
                    {
                        ImageClass.ImageMethod(picture, "FitnessUser", out string bigImageName, out string smallImageName, out string tempData);

                        if (tempData != null)
                        {
                            TempData["warning"] = tempData;
                        }

                        System.IO.File.Delete(Server.MapPath("~/Uploads/FitnessUser/" + fitnessUser.SmallImageName));
                        System.IO.File.Delete(Server.MapPath("~/Uploads/FitnessUser/" + fitnessUser.BigImageName));

                        fitnessUser.BigImageName = bigImageName;
                        fitnessUser.SmallImageName = smallImageName;
                    }
                    UserDao uDao = new UserDao();

                    fitnessUser.Role = new RoleDao().GetById(roleId);

                    uDao.Update(fitnessUser);
                }
                else
                {
                    return View("EditEmployee", fitnessUser);
                }
                if (TempData["warning"] == null)
                {
                    TempData["succes"] = "Založení zaměstnance proběhlo úspěšně.";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return RedirectToAction("Index");
        }

        public ActionResult EditLog(int id)
        {
            UserDao uDao = new UserDao();
            FitnessUser fitnessUser = uDao.GetById(id);

            return View(fitnessUser);
        }

        [HttpPost]
        public ActionResult UpdateLog(FitnessUser fitnessUser)
        {
            fitnessUser.Password = PasswordHash.CreateHash(fitnessUser.Password);

            UserDao uDao = new UserDao();
            uDao.Update(fitnessUser);

            TempData["message-success"] = "Uživatel " + fitnessUser.Name + " byl úspěšně upraven";

            return RedirectToAction("Logout", "Home");
        }

        [Authorize(Roles = "Ředitel, Zákazník")]
        public ActionResult Delete(int id)
        {
            try
            {

                UserDao uDao = new UserDao();
                AddressDao aDao = new AddressDao();
                FitnessUser fitnessUser = uDao.GetById(id);
                Address address = fitnessUser.Address;

                //pokud se jedná o trenéra, je třeba smazat jeho termíny, které vede
                if (fitnessUser.Role.Name == "Trenér")
                {
                    TermDao tDao = new TermDao();
                    IList<Term> terms = tDao.GetTermsByTrainer(fitnessUser);

                    foreach (Term t in terms)
                    {
                       tDao.Delete(t);      
                    }
                }//pokud se jedna o zakaznika, je treba smazat rezervace, na ktere je zapsan
                else if (fitnessUser.Role.Name == "Zákazník")
                {
                    ReservationDao rDao = new ReservationDao();
                    IList<Reservation> reservations = rDao.GetAllReservationsByUser(fitnessUser);

                    foreach (Reservation r in reservations)
                    {
                        rDao.Delete(r);
                    }
                }
                //todo pokud se bude jednat o přihlášeného uživatele, je třeba jej odhlásit, 
                //todo zaměstnance bude moci mazat pouze reditel, možná pouvažovat na samostatné metody mazání
                aDao.Delete(address);
                uDao.Delete(fitnessUser);

                TempData["succes"] = "Uživatelský účet byl odstraněn.";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeletePicture(int id, string view)
        {
            try
            {
                UserDao uDao = new UserDao();
                FitnessUser u = uDao.GetById(id);

                System.IO.File.Delete(Server.MapPath("~/Uploads/FitnessUser/" + u.SmallImageName));
                System.IO.File.Delete(Server.MapPath("~/Uploads/FitnessUser/" + u.BigImageName));

                u.SmallImageName = null;
                u.BigImageName = null;

                TempData["succes"] = "Obrázek stroje odstraněn.";

                if (view == "Emp")
                {
                    return View("EditEmployee", u);
                }

                return View("EditUser", u);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}