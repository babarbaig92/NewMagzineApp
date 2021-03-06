﻿using NewMagzineApp.AppCode;
using NewMagzineApp.AppCode.BAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace NewMagzineApp
{
    public partial class ImageEditor : System.Web.UI.Page
    {
        protected string imageLocation = "";
        protected string pageImageLocation = "";
        protected string originalImageName = "";
        protected int originalImageId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            imageLocation = Server.MapPath(" ") + "\\images\\";
            pageImageLocation = Server.MapPath(" ") + "\\MagzineAppFiles\\";
            GetParameters();

            if (!Page.IsPostBack)
            {
                LoadImageFromDB(); // Load Image from DB Before Editing
            }
            LoadImageIntoPage();
        }
        private void GetParameters()
        {
            originalImageName = Request.Params.Get("imagePageName"); // Original image should be available already in images directory
            //originalImageName = "1.jpg";
            originalImageId = Convert.ToInt32(Request.Params.Get("imagePageId")); // Original image should be available already in images directory
            hdnOriginalImageId.Value = Convert.ToString(originalImageId);
            hdnOriginalImageName.Value = Convert.ToString(originalImageName);
        }
        private void LoadImageFromDB()
        {
            DocumentHelper dh = new DocumentHelper();
            byte[] pageImageContent = dh.GetPageImageForSectionExtraction(originalImageId);

            System.IO.File.WriteAllBytes(pageImageLocation + originalImageName, pageImageContent);
            //Bitmap pageImage = null;
            //using (var ms = new MemoryStream(pageImageContent))
            //{
            //    pageImage = new Bitmap(ms);
            //}
            //pageImage.Save(pageImageLocation + originalImageName, ImageFormat.Png);
        }

        private void LoadImageIntoPage()
        {
            if (File.Exists(pageImageLocation + originalImageName))
            {
                HtmlImage img = new HtmlImage();
                img.ID = hdnOriginalImageId.Value;
                img.Src = "MagzineAppFiles\\" + originalImageName;
                img.Width = 700;
                img.Height = 900;
                imageContainer.Controls.Add(img);
            }
        }

        #region Image Section Saving
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
            string originalFilePath = pageImageLocation + imgPart.OriginalImageName;
            Bitmap originalMap = new Bitmap(originalFilePath);
            imgPart.ImagePartName = Guid.NewGuid().ToString() + ".png";
            imgPart.ImagePartByte = ExtractImageSection(originalMap, imgPart);
            return imgPart;
        }

        private byte[] ExtractImageSection(Bitmap originalMap, ImagePart imgPart)
        {
            Bitmap imageSection = new Bitmap(imgPart.Width, imgPart.Height);

            int top = Math.Min(imgPart.Y1, imgPart.Y2);
            int bottom = Math.Max(imgPart.Y1, imgPart.Y2);
            int left = Math.Min(imgPart.X1, imgPart.X2);
            int right = Math.Max(imgPart.X1, imgPart.X2);

            Rectangle cloneRect = Rectangle.FromLTRB(left, top, right, bottom);
            Graphics g = Graphics.FromImage(imageSection);

            Point firstCoord = new Point(imgPart.X1, imgPart.Y1);
            Point secondCoord = new Point(imgPart.X2, imgPart.Y2);

            // srcY = bottom
            // Draw the given area (section) of the source image
            // at location 0,0 on the empty bitmap (imageSection)
            //g.DrawImage(originalMap, 0, 0, cloneRect, GraphicsUnit.Pixel);

            //working
            //g.DrawImage(originalMap, cloneRect,0, 0, imgPart.Width, imgPart.Height, GraphicsUnit.Pixel);

            int srcX = cloneRect.X;
            int srcY = cloneRect.Y;

            g.DrawImage(
                originalMap,
                cloneRect,
                srcX,
                srcY,
                imgPart.Width,
                imgPart.Height,
                GraphicsUnit.Pixel);


            /*
            int top = Math.Min(imgPart.Y1, imgPart.Y2);
            int bottom = Math.Max(imgPart.Y1, imgPart.Y2);
            int left = Math.Min(imgPart.X1, imgPart.X2);
            int right = Math.Max(imgPart.X1, imgPart.X2);

            Rectangle cloneRect = Rectangle.FromLTRB(left, top, right, bottom);
            System.Drawing.Imaging.PixelFormat pixelFormat = originalMap.PixelFormat;
            Bitmap imageSection = (Bitmap)originalMap.Clone(cloneRect, pixelFormat);*/

            imageSection.Save(pageImageLocation + imgPart.ImagePartName, ImageFormat.Png);
            byte[] imageSectionByte = ConvertBitmapIntoByte(imageSection);
            return imageSectionByte;
        }

        private byte[] ConvertBitmapIntoByte(Bitmap source)
        {
            ImageConverter imageConverter = new ImageConverter();
            return (byte[])imageConverter.ConvertTo(source, typeof(byte[]));
        }

        #endregion


        [System.Web.Services.WebMethod]
        public static string SaveCroppedData(string name)
        {
            return "Hello " + name + Environment.NewLine + "The Current Time is: "
                + DateTime.Now.ToString();
        }



    }
}