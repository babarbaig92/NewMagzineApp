using NewMagzineApp.AppCode;
using NewMagzineApp.AppCode.BAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewMagzineApp
{
    public partial class ImageEditor : System.Web.UI.Page
    {
        protected string imageLocation = "";
        protected string originalImageName = "";
        protected int originalImageId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            imageLocation = Server.MapPath(" ") + "\\images\\";
            GetParameters();

        }

        private void GetParameters()
        {
            // originalImageName = Request.Params.Get("imagePageName"); // Original image should be available already in images directory
            originalImageName = "1.jpg";
            originalImageId = Convert.ToInt32(Request.Params.Get("imagePageId")); // Original image should be available already in images directory

            hdnOriginalImageId.Value = Convert.ToString(originalImageId);
            hdnOriginalImageName.Value = Convert.ToString(originalImageName);


        }

        protected void btnSavePart_Click(object sender, EventArgs e)
        {
            ImagePart imageSection = PrepareImageSectionToSave();
            DocumentHelper documentHelper = new DocumentHelper();
            documentHelper.SaveImageSection(imageSection);


            // imageSection.Save(imageLocation + imgPart.ImagePartName); // Save part to local
        }

        private ImagePart PrepareImageSectionToSave()
        {
            string imagePartJSON = Request.Params.Get("hdnImagePart"); // Image section data from front-end
            ImagePart imgPart = new JavaScriptSerializer().Deserialize<ImagePart>(imagePartJSON);
            string originalFilePath = imageLocation + imgPart.OriginalImageName;
            Bitmap originalMap = new Bitmap(originalFilePath);
            imgPart.ImagePartName = Guid.NewGuid().ToString() + ".jpg";
            imgPart.ImagePartByte = ExtractImageSection(originalMap, imgPart);
            return imgPart;
        }

        private byte[] ExtractImageSection(Bitmap originalMap, ImagePart imgPart)
        {
            int top = Math.Min(imgPart.Y1, imgPart.Y2);
            int bottom = Math.Max(imgPart.Y1, imgPart.Y2);
            int left = Math.Min(imgPart.X1, imgPart.X2);
            int right = Math.Max(imgPart.X1, imgPart.X2);

            Rectangle cloneRect = Rectangle.FromLTRB(left, top, right, bottom);
            System.Drawing.Imaging.PixelFormat pixelFormat = originalMap.PixelFormat;
            Bitmap imageSection = originalMap.Clone(cloneRect, pixelFormat);
            byte[] imageSectionByte = ConvertBitmapIntoByte(imageSection);
            return imageSectionByte;
        }

        private byte[] ConvertBitmapIntoByte(Bitmap source)
        {
            ImageConverter imageConverter = new ImageConverter();
            return (byte[])imageConverter.ConvertTo(source, typeof(byte[]));
        }


    }
}