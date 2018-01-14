using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace FitnessCenter.Class
{
    public class ImageClass
    {
        public static Image ScaleImage(Image image, int maxHeight)
        {
            var ratio = (double)maxHeight / image.Height;

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }

        private static void SaveImage(Image image, string controllerName,  out string imageName)
        {
            Bitmap b = new Bitmap(image);

            Guid g = Guid.NewGuid();

            imageName = g.ToString();
            b.Save(HttpContext.Current.Server.MapPath("~/Uploads/" + controllerName + "/" + imageName + ".jpg"),
                ImageFormat.Jpeg);

            b.Dispose();

    }

        public static void ImageMethod(HttpPostedFileBase picture, string controllerName, out string bImageName, out string sImageName, out string tempData)
        {
            bImageName = null;
            sImageName = null;
            tempData = null;

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
                    bigImage = ScaleImage(image, 800);
                    smallImage = ScaleImage(image, 200);
                }
                else if (image.Width > 200 || image.Height > 200)
                {
                    smallImage = ScaleImage(image, 200);
                    bigImage = image;

                    tempData = "Obrázek pro detail není dostatečně velký.";
                }
                else
                {
                    tempData = "Příliš malý obrázek. Nebylo možné ho přidat k profilu uživatele.";
                }
                if (smallImage != null)
                {
                    SaveImage(smallImage, controllerName, out string sIName);
                    sImageName = sIName;
                }
                if (bigImage != null)
                {
                    SaveImage(bigImage, controllerName, out string bIName);
                    bImageName = bIName;
                }
            }
            else
            {
                tempData = "Nepodporovaný typ obrázku.";
            }

        }
    }
}