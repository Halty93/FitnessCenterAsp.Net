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
            IList<Machine> machs = mDao.GetAll();
            return View(machs);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Machine mach, HttpPostedFileBase picture)
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

                        if (image.Width > 1024 || image.Height > 1024)
                        {
                            bigImage = ImageClass.ScaleImage(image, 1024);
                            smallImage = ImageClass.ScaleImage(image, 256);
                        }
                        else if (image.Width >= 256 || image.Height >= 256)
                        {
                            smallImage = ImageClass.ScaleImage(image, 256);
                            bigImage = image;

                            TempData["warning"] = "Obrázek pro detail není dostatečně velký.";
                        }
                        else
                        {
                            TempData["warning"] = "Příliš malý obrázek. Nebylo možné ho přidat ke stroji.";
                        }
                        if (smallImage != null)
                        {
                            ImageClass.SaveImage(smallImage, "Machine", out var SIName);
                            mach.SmallImageName = SIName;
                        }
                        else if (bigImage != null)
                        {
                            ImageClass.SaveImage(bigImage, "Machine", out var BIName);
                            mach.BigImageName = BIName;

                        }
                    }
                }
                //pridat stroj
            }
            else
            {
                return View("Create", mach);
            }
            TempData["succes"] = "Nový stroj úspěšně přidán.";
            return RedirectToAction("Index");
        }
    }
}