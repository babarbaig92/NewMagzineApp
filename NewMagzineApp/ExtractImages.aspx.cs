using IronPdf;
using NewMagzineApp.AppCode.BAL;
using NewMagzineApp.AppCode.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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

            filePath = Server.MapPath(" ") + "\\MagzineAppFiles\\"; // Temporary storage location for file that is fetched from db to convert into images
            DocumentId = Convert.ToInt32(Request.Params.Get("DocumentId"));
            DocumentName = Convert.ToString(Request.Params.Get("DocumentName"));

            if(!Page.IsPostBack)
            {
                SavePDFIntoImages();
            }
            
        }

        private void SavePDFIntoImages()
        {
            Bitmap[] pdfPages = GetPageImages();
            DocumentHelper documentHelper = new DocumentHelper();

            List<PageImage> pageImages = new List<PageImage>();
            for(int pageNumber = 0; pageNumber < pdfPages.Length; pageNumber++)
            {
                PageImage page = new PageImage();
                byte[] pageByte = ConvertBitmapIntoByte(pdfPages[pageNumber]);
                page.PageBinaryData = pageByte;
                page.OriginalDocumentId = DocumentId;
                page.DocumentPageNumber = pageNumber + 1;
                page.DocumentImageName = Guid.NewGuid().ToString() + ".png";
                pageImages.Add(page);
            }
            List<int> pageImageIds = documentHelper.SavePDFImagePage(pageImages);
            CleanDirectory();

            #region  Display Image Section on Original Image
            var lineBreak = new HtmlGenericControl("br");
            for (int counter = 0; counter < pdfPages.Length; counter++)
            {
                HtmlImage img = new HtmlImage();
                img.Width = 700;
                img.Height = 900;
                img.ID = "img_" + pageImageIds[counter];

                MemoryStream ms = new MemoryStream();
                pdfPages[counter].Save(ms, ImageFormat.Png);
                var base64Data = Convert.ToBase64String(ms.ToArray());
                img.Src = "data:image/Png;base64," + base64Data;
                img.Attributes.Add("onclick", "javascript:TransferImageForEditing('" + pageImageIds[counter] + "','" + pageImages[counter].DocumentImageName  + "');");
                //img.Attributes.Add("usemap", "#mapname");
                

                var h3 = new HtmlGenericControl("h3");
                h3.InnerHtml = "Page " + (counter + 1);
                imageContainer.Controls.Add(h3);
                imageContainer.Controls.Add(lineBreak);

                //HtmlGenericControl map = new HtmlGenericControl("map");
                //map.Attributes.Add("name", "mapname");
                //HtmlGenericControl area = new HtmlGenericControl("area");
                //area.Attributes.Add("shape", "rect");
                //area.Attributes.Add("coords", "114,346, 150, 100");
                //area.Attributes.Add("href", "MagzineAppFiles/1_a.png");
                //area.Attributes.Add("alttext", "image" + counter);
                //area.Attributes.Add("target", "_blank");
                //map.Controls.Add(area);

                imageContainer.Controls.Add(img);
                //imageContainer.Controls.Add(map);
            }
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
        private byte[] ConvertBitmapIntoByte(Bitmap source)
        {
            ImageConverter imageConverter = new ImageConverter();
            return (byte[])imageConverter.ConvertTo(source, typeof(byte[]));
        }

        protected void hdnTransferImageForEditing_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("ImageEditor.aspx?imagePageId=" + hdnImageId.Value + "&imagePageName=" + hdnImageName.Value);
        }
    }
}