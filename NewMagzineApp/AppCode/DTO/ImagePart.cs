using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewMagzineApp.AppCode
{
    public class ImagePart
    {
        public int OriginalImageId { get; set; }
        public string OriginalImageName { get; set; }
        public string ImagePartName { get; set; }
        public byte[] ImagePartByte { get; set; }
        public int X1 { get; set; }
        public int X2 { get; set; }
        public int Y1 { get; set; }
        public int Y2 { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}