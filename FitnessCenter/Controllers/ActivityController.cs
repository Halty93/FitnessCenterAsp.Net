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
            IList<Activity> acts= ad.GetAll();
            return View(acts);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Activity act, HttpPostedFileBase picture)
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
                            TempData["warning"] = "Příliš malý obrázek.Nebylo možné ho přidat k aktivitě.";
                        }
                        if (smallImage != null)
                        {
                            ImageClass.SaveImage(smallImage, "Activity", out var SIName);
                            act.SmallImageName = SIName;
                        }
                        else if (bigImage != null)
                        {
                            ImageClass.SaveImage(bigImage, "Activity", out var BIName);
                            act.BigImageName = BIName;
                        }
                    }
                }
                //act.Id = Activity.FakeList.Count + 1;
                //Activity.FakeList.Add(act);
            }
            else
            {
                return View("Create", act);
            }
            TempData["succes"] = "Nová aktivita úspěšně přidána.";
            return RedirectToAction("Index");
        }
    }
}