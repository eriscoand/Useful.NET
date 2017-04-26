using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.ViewModels
{
    public class ImageCrop
    {
        public int x1 { get; set; }
        public int y1 { get; set; }
        public int x2 { get; set; }
        public int y2 { get; set; }
        public int w { get; set; }
        public int h { get; set; }
        public double ratio { get; set; }
        public int desired_width { get; set; }

        public ImageCrop()
        {
            this.x1 = 0;
            this.y1 = 0;
            this.x2 = 0;
            this.y2 = 0;
            this.w = 0;
            this.h = 0;
            this.ratio = 1;
            this.desired_width = 0;
        }

        public ImageCrop(string x1, string y1, string x2, string y2, string w, string h, string ratio, int desired_width)
        {
            this.x1 = FormParser.StringToInt(x1);
            this.y1 = FormParser.StringToInt(y1);
            this.x2 = FormParser.StringToInt(x2);
            this.y2 = FormParser.StringToInt(y2);
            this.w = FormParser.StringToInt(w);
            this.h = FormParser.StringToInt(h);
            this.ratio = FormParser.StringToDouble(ratio);
            this.desired_width = desired_width;
        }


    }
}
