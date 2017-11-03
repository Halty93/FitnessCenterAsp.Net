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
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            UserDao uDao = new UserDao();
            IList <User> users = uDao.GetAll();
            return View(users);
        }

        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(User user, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                if (picture != null)
                {
                    if (picture.ContentType == "image/jpeg" ||
                        picture.ContentType == "image/png" ||
                        picture.ContentType == "image/bmp" ||
                        picture.ContentType == "image/gif")
                    {
                        Image image = Image.FromStream(picture.InputStream);
                        Image smallImage = null;
                        Image bigImage = null;

                        if (image.Width > 800 || image.Height > 800)
                        {
                            bigImage = ImageClass.ScaleImage(image, 800);
                            smallImage = ImageClass.ScaleImage(image, 200);
                        }
                        else if (image.Width > 200 || image.Height > 200)
                        {
                            smallImage = ImageClass.ScaleImage(image, 200);
                            bigImage = image;

                            TempData["warning"] = "Obrázek pro detail není dostatečně velký.";
                        }
                        else
                        {
                            TempData["warning"] = "Příliš malý obrázek. Nebylo možné ho přidat k profilu uživatele.";
                        }
                        if (smallImage != null)
                        {
                            ImageClass.SaveImage(smallImage, "User", out var SIName);
                            user.SmallImageName = SIName;
                        }
                        else if (bigImage != null)
                        {
                            ImageClass.SaveImage(bigImage, "User", out var BIName);
                            user.BigImageName = BIName;

                        }
                    }
                }
                //přidat uživatele
            }
            else
            {
                return View("CreateUser", user);
            }
            TempData["succes"] = "Registrace proběhla úspěšně.";
            return RedirectToAction("Index");
        }
    }
}