using IronPdf;
using NewMagzineApp.AppCode.BAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewMagzineApp
{
    public partial class ExtractImages : System.Web.UI.Page
    {
        private int DocumentId = 0;
        private string DocumentName = "";
        private string filePath = "";
        protected void Page_Load(object sender, EventArgs e)
        {            
            // Do not generate pages if already exists (add bit in document table to check if pages have been generated already)

            filePath = Server.MapPath(" ") + "\\MagzineAppFiles\\";
            DocumentId = Convert.ToInt32(Request.Params.Get("DocumentId"));
            DocumentName = Convert.ToString(Request.Params.Get("DocumentName"));
            //SavePDFIntoImages();
            //OpenFileInPhotoEditor();
        }

        private void SavePDFIntoImages()
        {

            Bitmap[] pdfPages = GetPageImages();

            // Save pdfPages to DB
            DocumentHelper documentHelper = new DocumentHelper();
            documentHelper.SavePDFImagePage(pdfPages[0]);





            #region  Display Image Section on Original Image
            //var lineBreak = new HtmlGenericControl("br");
            //for (int counter = 0; counter < pdfPages.Length - 3; counter++)
            //{
            //    HtmlImage img = new HtmlImage();
            //    img.Width = 600;
            //    img.Height = 900;
            //    img.ID = "img_" + counter;

            //    MemoryStream ms = new MemoryStream();
            //    pdfPages[counter].Save(ms, ImageFormat.Png);
            //    var base64Data = Convert.ToBase64String(ms.ToArray());
            //    img.Src = "data:image/png;base64," + base64Data;
            //    img.Attributes.Add("usemap", "#mapname");


            //    var h3 = new HtmlGenericControl("h3");
            //    h3.InnerHtml = "Page " + counter + 1;
            //    imageContainer.Controls.Add(h3);
            //    imageContainer.Controls.Add(lineBreak);

            //    HtmlGenericControl map = new HtmlGenericControl("map");
            //    map.Attributes.Add("name", "mapname");
            //    HtmlGenericControl area = new HtmlGenericControl("area");
            //    area.Attributes.Add("shape", "rect");
            //    area.Attributes.Add("coords", "114,346, 150, 100");
            //    area.Attributes.Add("href", "MagzineAppFiles/1_a.png");
            //    area.Attributes.Add("alttext", "image" + counter);
            //    area.Attributes.Add("target", "_blank");
            //    map.Controls.Add(area);

            //    imageContainer.Controls.Add(img);
            //    imageContainer.Controls.Add(map);
            //}
            //CleanDirectory();

            #endregion
        }

        private void CleanDirectory()
        {
            string[] filePaths = Directory.GetFiles(filePath);
            foreach (string filePath in filePaths)
                File.Delete(filePath);
        }
        private Bitmap[] GetPageImages()
        {
            // Load document related to docid in server path So that images could be generated
            GetPDFDocumentFromDB();
            
            //Rendering PDF document (placed at server path) to images
            var pdf = PdfDocument.FromFile(filePath + DocumentName);
            //Extract all pages to a folder as image files

            //pdf.RasterizeToImageFiles(filePath + "*.png");

            //Dimensions and page ranges may be specified
            //pdf.RasterizeToImageFiles(filePath + "thumbnail_*.jpg", 100, 80);
            //Extract all pages as System.Drawing.Bitmap objects
            Bitmap[] pageImages = pdf.ToBitmap();
            return pageImages;
        }

        private void GetPDFDocumentFromDB()
        {
            DocumentHelper dh = new DocumentHelper();
            byte[] pdfByte = dh.GetDocumentBinary(DocumentId);
            System.IO.File.WriteAllBytes(filePath + DocumentName, pdfByte);
        }
    }
}