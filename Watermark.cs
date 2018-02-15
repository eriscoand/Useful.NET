using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

public static class Watermark
{
    public static byte[] apply(byte[] source)
    {
        try
        {

            var url = HttpContext.Current.Server.MapPath("/Content/images/logo.png");

            using (var stream = new MemoryStream(source))
            using (Image image = Image.FromStream(stream))
            using (Image watermarkImage = Image.FromFile(url))
            using (Graphics imageGraphics = Graphics.FromImage(image))
            using (TextureBrush watermarkBrush = new TextureBrush(watermarkImage))
            {
                int x = (image.Width / 2 - watermarkImage.Width / 2);
                int y = (image.Height / 2 - watermarkImage.Height / 2);
                watermarkBrush.TranslateTransform(x, y);
                imageGraphics.FillRectangle(watermarkBrush, new Rectangle(new Point(x, y), new Size(watermarkImage.Width + 1, watermarkImage.Height)));

                using (var streamAux = new MemoryStream())
                {
                    image.Save(streamAux, image.RawFormat);
                    return streamAux.ToArray();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    
}