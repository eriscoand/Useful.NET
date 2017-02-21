using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Log
{
    public static void SimpleLog(string message)
    {
        string fileLog = System.Web.HttpContext.Current.Server.MapPath("~/Log/");
        fileLog = fileLog + string.Format("{0}.txt", DateTime.Now.ToString("yyyyMMdd"));

        string text = "[" + DateTime.Now.ToString("HH:mm:ss") + "] Error: " + message.ToString() + "";

        if (!File.Exists(fileLog))
        {
            using (StreamWriter sw = File.CreateText(fileLog))
            {
                sw.WriteLine("----------------START----------------");
            }
        }

        using (StreamWriter sw = File.AppendText(fileLog))
        {
            sw.WriteLine(text);
        }	

    }
}
