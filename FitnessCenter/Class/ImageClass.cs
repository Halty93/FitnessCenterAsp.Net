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

        public static void SaveImage(Image image, string ControllerName, out string imageName)
        {
            Bitmap b = new Bitmap(image);

            Guid g = Guid.NewGuid();

            imageName = g.ToString() + ".jpg";
            b.Save(HttpContext.Current.Server.MapPath("~/Uploads/"+ ControllerName +"/" + imageName + ".jpg"),ImageFormat.Jpeg);

            b.Dispose();
        }


    }
}