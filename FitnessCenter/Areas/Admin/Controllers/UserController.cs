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
            AddressDao aDao = new AddressDao();
            
            UserDao uDao = new UserDao();
            IList<FitnessUser> users = uDao.GetUserPage(itemsOnPage, pg);
            ViewBag.Pages = (int)Math.Ceiling((double)uDao.GetAll().Count / (double)itemsOnPage);
            ViewBag.CurrentPage = pg;
            ViewBag.Items = itemsOnPage;
            ViewBag.Roles = new RoleDao().GetAll();
            ViewBag.Mark = "User";
            if (Request.IsAjaxRequest())
            {
                return PartialView(users);
            }
            return View(users);
        }

        public ActionResult Detail(int id)
        {
            UserDao uDao = new UserDao();
            FitnessUser user = uDao.GetById(id);
            ViewBag.CurrentUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            ViewBag.Mark = "User";
            if (Request.IsAjaxRequest())
            {
                return PartialView(user);
            }
            return View(user);
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
            if (Request.IsAjaxRequest())
            {
                return PartialView("Index", users);
            }
            return View("Index", users);
        }

        public ActionResult CreateUser()
        {
            ViewBag.Mark = "User";
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
                    fitnessUser.Password = PasswordHash.CreateHash(fitnessUser.Password);
                    a = fitnessUser.Address;

                    if (uDao.LoginExist(fitnessUser.Login) == false)
                    {
                        aDao.Create(a); 
                        fitnessUser.Address = a;
                        uDao.Create(fitnessUser);
                    }
                    else
                    {
                        TempData["warning"] = "Uživatel pod tímto loginem již existuje!";
                        return View("CreateUser", fitnessUser);
                    }
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
            ViewBag.Mark = "User";

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
                    a = fitnessUser.Address;
                    if (uDao.LoginExist(fitnessUser.Login) == false)
                    {
                        aDao.Create(a);
                        fitnessUser.Address = a;
                        uDao.Create(fitnessUser);                      
                    }
                    else
                    {
                        TempData["warning"] = "Uživatel pod tímto loginem již existuje!";
                        return View("CreateEmployee", fitnessUser);
                    }
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
            ViewBag.Mark = "User";

            return View(uDao.GetById(id));
        }

        [HttpPost]
        public ActionResult UpdateAddress(FitnessUser fitnessUser, int roleId, int addressId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserDao uDao = new UserDao();
                    RoleDao rDao = new RoleDao();
                    AddressDao aDao = new AddressDao();
                    Address a = new Address();

                    fitnessUser.Address.Id = addressId;
                    fitnessUser.Role = rDao.GetById(roleId);

                    a.Id = addressId;
                    a.Country = fitnessUser.Address.Country;
                    a.Street = fitnessUser.Address.Street;
                    a.StreetNumber = fitnessUser.Address.StreetNumber;
                    a.Town = fitnessUser.Address.Town;
                    a.Zip = fitnessUser.Address.Zip;

                    
                    aDao.Update(a);
                    uDao.Update(fitnessUser);

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
            ViewBag.Mark = "User";
            return View(uDao.GetById(id));
        }

        [HttpPost]
        public ActionResult UpdateUser(FitnessUser fitnessUser, HttpPostedFileBase picture, int roleId, int addressId)
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

                        if (fitnessUser.SmallImageName != null)
                        {
                            System.IO.File.Delete(Server.MapPath("~/Uploads/FitnessUser/" + fitnessUser.SmallImageName));
                        }
                        if (fitnessUser.BigImageName != null)
                        {
                            System.IO.File.Delete(Server.MapPath("~/Uploads/FitnessUser/" + fitnessUser.BigImageName));
                        }                        

                        fitnessUser.BigImageName = bigImageName;
                        fitnessUser.SmallImageName = smallImageName;
                    }
                    UserDao uDao = new UserDao();
                    RoleDao rDao = new RoleDao();
                    AddressDao aDao = new AddressDao();

                    fitnessUser.Role = rDao.GetById(roleId);
                    fitnessUser.Address = aDao.GetById(addressId);

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
            ViewBag.Mark = "User";

            return View(uDao.GetById(id));
        }

        [HttpPost]
        public ActionResult UpdateEmployee(FitnessUser fitnessUser, HttpPostedFileBase picture, int roleId, int addressId)
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

                        if (fitnessUser.SmallImageName != null)
                        {
                            System.IO.File.Delete(Server.MapPath("~/Uploads/FitnessUser/" + fitnessUser.SmallImageName));
                        }
                        if (fitnessUser.BigImageName != null)
                        {
                            System.IO.File.Delete(Server.MapPath("~/Uploads/FitnessUser/" + fitnessUser.BigImageName));
                        }

                        fitnessUser.BigImageName = bigImageName;
                        fitnessUser.SmallImageName = smallImageName;
                    }
                    UserDao uDao = new UserDao();
                    RoleDao rDao = new RoleDao();
                    AddressDao aDao = new AddressDao();

                    fitnessUser.Role = rDao.GetById(roleId);
                    fitnessUser.Address = aDao.GetById(addressId);

                    uDao.Update(fitnessUser);
                }
                else
                {
                    return View("EditEmployee", fitnessUser);
                }
                if (TempData["warning"] == null)
                {
                    TempData["succes"] = "Úprava zaměstnance proběhlo úspěšně.";
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
            ViewBag.Mark = "User";

            return View(fitnessUser);
        }

        [HttpPost]
        public ActionResult UpdateLog(FitnessUser fitnessUser, int roleId, int addressId)
        {
            UserDao uDao = new UserDao();
            FitnessUser u = uDao.GetById(fitnessUser.Id);
            bool LoginExist = false;

            fitnessUser.Address = new AddressDao().GetById(addressId);
            fitnessUser.Role = new RoleDao().GetById(roleId);
            fitnessUser.Password = PasswordHash.CreateHash(fitnessUser.Password);


            if(fitnessUser.Login !=u.Login)
            {
                LoginExist = uDao.LoginExist(fitnessUser.Login);
            }

            if (LoginExist==false)
            {
                uDao.Update(fitnessUser);
                TempData["message-success"] = "Uživatel " + fitnessUser.Name + " byl úspěšně upraven";
            }
            else
            {
                TempData["warning"] = "Uživatel pod tímto loginem již existuje!";
                return View("EditLog", fitnessUser);
            }

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
                Address address = new Address();
                address = fitnessUser.Address;

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

                if (fitnessUser.SmallImageName != null)
                {
                    System.IO.File.Delete(Server.MapPath("~/Uploads/FitnessUser/" + fitnessUser.SmallImageName));
                }
                if (fitnessUser.BigImageName != null)
                {
                    System.IO.File.Delete(Server.MapPath("~/Uploads/FitnessUser/" + fitnessUser.BigImageName));
                }

                uDao.Delete(fitnessUser);
                aDao.Delete(address);
                

                TempData["succes"] = "Uživatelský účet byl odstraněn.";

                if (fitnessUser.Login == User.Identity.Name)
                {
                    return RedirectToAction("Logout", "Home");
                }
                    return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public ActionResult DeletePicture(int id, string view)
        {
            try
            {
                UserDao uDao = new UserDao();
                FitnessUser u = uDao.GetById(id);

                if (u.SmallImageName != null)
                {
                    System.IO.File.Delete(Server.MapPath("~/Uploads/FitnessUser/" + u.SmallImageName));
                }
                if (u.BigImageName != null)
                {
                    System.IO.File.Delete(Server.MapPath("~/Uploads/FitnessUser/" + u.BigImageName));
                }

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