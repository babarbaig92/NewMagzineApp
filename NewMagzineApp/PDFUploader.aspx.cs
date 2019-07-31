using NewMagzineApp.AppCode.BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewMagzineApp
{
    public partial class PDFUploader : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadUploadedDocuments();
            }
        }

        private void LoadUploadedDocuments()
        {
            DocumentHelper documentHelper = new DocumentHelper();
            DataTable table = documentHelper.GetUploadedDocuments("", "");
            savedFilesList.DataSource = table;
            savedFilesList.DataBind();
        }

        protected void btnSaveFile_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            if (fileUploader.HasFile)
            {
                try
                {
                    sb.AppendFormat(" Uploading file: {0}", fileUploader.FileName);
                    string filePath = Server.MapPath(" ") + "\\MagzineAppFiles\\";
                    //fileUploader.SaveAs(filePath + fileUploader.FileName);
                    int docId = InsertDocument(fileUploader);
                    if (!docId.Equals(0))
                    {
                        sb.AppendFormat("<br/> File Added Successfully");
                        sb.AppendFormat("<br/> File Id: {0}", docId);
                    }
                    else
                    {
                        sb.AppendFormat("<br/> Some Error in Uploading File");
                    }
                }
                catch (Exception ex)
                {
                    sb.Append("<br/> Error <br/>");
                    sb.AppendFormat("Unable to save file <br/> {0}", ex.Message);
                }
            }
            //lblmessage.Text = sb.ToString();
            Response.Redirect("PDFUploader.aspx");
        }


        private int InsertDocument(FileUpload fileUploader)
        {
            byte[] fileBinaryContent = GetFileBinay(fileUploader.PostedFile.InputStream);

            DocumentHelper documentHelper = new DocumentHelper();
            return documentHelper.InsertDocument(fileUploader.PostedFile.FileName, fileUploader.PostedFile.ContentType,
                Convert.ToString(fileBinaryContent.Length), fileBinaryContent, false);
        }

        private byte[] GetFileBinay(Stream inputStream)
        {
            using (Stream fs = inputStream)
            {
                using (BinaryReader binarReader = new BinaryReader(fs))
                {
                    return binarReader.ReadBytes((Int32)fs.Length);
                }
            }
        }
        protected void btnRedirectToPDFProcessor_Click(object sender, ImageClickEventArgs e)
        {
            string documentId = hdnDocumentId.Value;
            string documentName = hdnDocumentName.Value;
            string hasImages = hdnHasImages.Value;
            Response.Redirect("ExtractImages.aspx?DocumentId=" + documentId + "&DocumentName=" + documentName + "&HasImages=" + hasImages);
        }












    }
}