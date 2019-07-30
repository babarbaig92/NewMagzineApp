using NewMagzineApp.AppCode;
using NewMagzineApp.AppCode.BAL;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace NewMagzineApp
{
    public partial class MagzineViewer : System.Web.UI.Page
    {
        protected string pageImageLocation = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get all document counts and id's of those documents
            // Get number of pages for each document and ids of those images
            // show list of documents and its pages on front end
            // on clicking a page open the page along with its section

            pageImageLocation = Server.MapPath(" ") + "\\MagzineAppFiles\\";
            int documentId = 1;
            int pageNumber = 1;
            int originalImageId = 48; // id of image whose sections will be fetched            
            byte[] mainPage = GetMagzinePage(documentId, pageNumber);
            List<ImagePart> imageSections = GetMagzinePageSections(originalImageId);

            CreateMapAreaForImagePage(mainPage, imageSections);

        }

        private void CreateMapAreaForImagePage(byte[] mainPage, List<ImagePart> imageSections)
        {
            string mainPageName = Guid.NewGuid().ToString() + ".png"; //can be replaced based on the data we fetch about original image
            System.IO.File.WriteAllBytes(pageImageLocation + mainPageName, mainPage);
            HtmlImage mainImage = new HtmlImage();
            mainImage.Width = 700;
            mainImage.Height = 900;
            mainImage.ID = "img_" + imageSections[0].OriginalImageId;
            mainImage.Src = "MagzineAppFiles\\" + mainPageName;
            mainImage.Attributes.Add("alt", "Main Image");
            mainImage.Attributes.Add("usemap", "pageMap");
            

            // HERE LOAD ALL IMAGE SECTIONS INTO pageImageLocation Path Folder

            HtmlGenericControl map = new HtmlGenericControl("map");
            map.Attributes.Add("name", "mapname");
            //HtmlGenericControl area = new HtmlGenericControl("area");
            //area.Attributes.Add("shape", "rect");
            //area.Attributes.Add("coords", "114,346, 150, 100");
            //area.Attributes.Add("href", "MagzineAppFiles/1_a.png");
            //area.Attributes.Add("alttext", "image" + counter);
            //area.Attributes.Add("target", "_blank");
            //map.Controls.Add(area);


            imageContainer.Controls.Add(mainImage);
            imageContainer.Controls.Add(map);
        }

        private List<ImagePart> GetMagzinePageSections(int originalImageId)
        {
            DocumentHelper dh = new DocumentHelper();
            return dh.GetMagzinePageSections(originalImageId);
        }

        private byte[] GetMagzinePage(int documentId, int pageNumber)
        {
            DocumentHelper dh = new DocumentHelper();
            return dh.GetMagzinePage(documentId, pageNumber);
        }



    }
}