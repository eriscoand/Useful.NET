using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

public class FileSaving
{

    public static List<FileInfo> getFiles(string folder)
    {
        DirectoryInfo directory = new DirectoryInfo(System.Web.Hosting.HostingEnvironment.MapPath(@"~\Images\icono"));
        return directory.GetFiles().ToList();
    }

    public static string saveFile(HttpPostedFileBase file, string folder, string name)
    {
        try
        {
            string targetFolder = System.Web.Hosting.HostingEnvironment.MapPath(folder);

            string extension = Path.GetExtension(file.FileName);

            string newFileName = CleanStrings.friendlyURL(name) + extension;

            string targetPath = Path.Combine(targetFolder, newFileName);

            deleteFile(folder + newFileName);
            file.SaveAs(targetPath);

            return newFileName;
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    public static string uploadAndCrop(HttpPostedFileBase imagen, string folder, ImageCrop crop)
    {
        if (imagen != null && imagen.ContentLength > 0)
        {
            Image img = Image.FromStream(imagen.InputStream, true, true);

            int x = System.Math.Min(crop.x1, crop.x2);
            int y = System.Math.Min(crop.y1, crop.y2);

            if (crop.w == 0 || crop.h == 0)
            {
                crop.w = img.Width;
                crop.h = img.Height;
            }

            int hdestino = Convert.ToInt32(System.Convert.ToDouble(crop.desired_width) / (System.Convert.ToDouble(crop.w) / System.Convert.ToDouble(crop.h)));

            using (System.Drawing.Bitmap _bitmap = new System.Drawing.Bitmap(crop.desired_width, hdestino))
            {
                _bitmap.SetResolution(img.HorizontalResolution, img.VerticalResolution);
                using (Graphics _graphic = Graphics.FromImage(_bitmap))
                {
                    _graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    _graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    _graphic.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    _graphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    _graphic.DrawImage(img, 0, 0, crop.desired_width, hdestino);
                    _graphic.DrawImage(img, new Rectangle(0, 0, crop.desired_width, hdestino), x, y, crop.w, crop.h, GraphicsUnit.Pixel);

                    string extension = Path.GetExtension(imagen.FileName);
                    var fileName = CleanStrings.friendlyURL(Path.GetFileName(imagen.FileName)) + "." + extension; 

                    string croppedFileName = Guid.NewGuid().ToString();
                    string path = System.Web.Hosting.HostingEnvironment.MapPath(folder);

                    string newFullPathName = string.Concat(path, croppedFileName, extension);

                    using (EncoderParameters encoderParameters = new EncoderParameters(1))
                    {
                        encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
                        _bitmap.Save(newFullPathName, GetImageCodec("." + extension), encoderParameters);
                    }
                    return croppedFileName + extension;
                }
            }
        }
        else
        {
            return "";
        }
    }

    public static void deleteFile(string url)
    {
        string fullPath = System.Web.Hosting.HostingEnvironment.MapPath(url);
        if (System.IO.File.Exists(fullPath))
        {
            System.IO.File.Delete(fullPath);
        }
    }

    private static ImageCodecInfo GetImageCodec(string extension)
    {
        extension = extension.ToUpperInvariant();
        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
        foreach (ImageCodecInfo codec in codecs)
        {
            if (codec.FilenameExtension.Contains(extension))
            {
                return codec;
            }
        }
        return codecs[1];
    }

    public static long getFileSize(string fileName, string folder)
    {
        var path = HttpRuntime.AppDomainAppPath + folder + fileName;
        try
        {
            return new System.IO.FileInfo(path).Length;
        }
        catch (Exception e)
        {
            return 0;
        }
    }
}
