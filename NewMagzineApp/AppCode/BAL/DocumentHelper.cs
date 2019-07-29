using NewMagzineApp.AppCode.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;

namespace NewMagzineApp.AppCode.BAL
{
    public class DocumentHelper
    {
        // TODO: Create new DB for storing pdf and images

        protected string connectionString = "";
        public DocumentHelper()
        {
            connectionString = ConfigurationManager.ConnectionStrings["MagzineAppConnection"].ConnectionString;
        }

        #region PDF Manipulation
        internal DataTable GetUploadedDocuments(string fromDate, string toDate)
        {
            DataTable documents = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GetUploadedDocuments";
                    cmd.Parameters.Add("@FromDate", SqlDbType.VarChar).Value = fromDate;
                    cmd.Parameters.Add("@ToDate", SqlDbType.VarChar).Value = toDate;
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    documents.Load(reader);
                }
            }
            return documents;
        }
        internal int InsertDocument(string DocumentName, string DocumentType, string DocumentLength, byte[] DocumentContent)
        {
            int documentId = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "InsertDocument";
                    cmd.Parameters.Add("@DocumentName", SqlDbType.VarChar).Value = DocumentName;
                    cmd.Parameters.Add("@DocumentType", SqlDbType.VarChar).Value = DocumentType;
                    cmd.Parameters.Add("@DocumentLength", SqlDbType.VarChar).Value = DocumentLength;
                    cmd.Parameters.Add("@DocumentContent", SqlDbType.VarBinary).Value = DocumentContent;

                    cmd.Parameters.Add("@DocumentId", SqlDbType.Int);
                    cmd.Parameters["@DocumentId"].Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    documentId = Convert.ToInt32(cmd.Parameters["@DocumentId"].Value);
                    conn.Close();
                }
            }
            return documentId;
        }
        internal byte[] GetDocumentBinary(int documentId)
        {
            DataTable document = new DataTable();
            byte[] pdfByte = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GetDocumentBinary";
                    cmd.Parameters.Add("@DocumentId", SqlDbType.Int).Value = documentId;
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    document.Load(reader);
                    conn.Close();
                }
            }

            if (document.Rows.Count > 0)
            {
                pdfByte = (byte[])document.Rows[0]["DocumentContent"];
            }
            return pdfByte;
        }
        #endregion

        #region Page Image Manipulation

        internal int SaveImageSection(ImagePart imageSection)
        {
            int imageSectionId = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SaveImageSection";
                    cmd.Parameters.Add("@OriginalImageId", SqlDbType.Int).Value = imageSection.OriginalImageId;
                    cmd.Parameters.Add("@ImagePartName", SqlDbType.VarChar).Value = imageSection.ImagePartName;
                    cmd.Parameters.Add("@ImagePartByte", SqlDbType.VarBinary).Value = imageSection.ImagePartByte;
                    cmd.Parameters.Add("@CoordX1", SqlDbType.Int).Value = imageSection.X1;
                    cmd.Parameters.Add("@CoordX2", SqlDbType.Int).Value = imageSection.X2;
                    cmd.Parameters.Add("@CoordY1", SqlDbType.Int).Value = imageSection.Y1;
                    cmd.Parameters.Add("@CoordY2", SqlDbType.Int).Value = imageSection.Y2;
                    cmd.Parameters.Add("@Width", SqlDbType.Int).Value = imageSection.Width;
                    cmd.Parameters.Add("@Height", SqlDbType.Int).Value = imageSection.Height;


                    cmd.Parameters.Add("@ImageSectionId", SqlDbType.Int);
                    cmd.Parameters["@ImageSectionId"].Direction = ParameterDirection.Output;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    imageSectionId = Convert.ToInt32(cmd.Parameters["@ImageSectionId"].Value);
                    conn.Close();
                }
            }
            return imageSectionId;
        }

        internal List<int> SavePDFImagePage(List<PageImage> pageImages)
        {
            List<int> pageImageIds = new List<int>();
            foreach (PageImage page in pageImages)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "InsertDocumentImages";
                        cmd.Parameters.Add("@OriginalDocumentId", SqlDbType.Int).Value = page.OriginalDocumentId;
                        cmd.Parameters.Add("@DocumentPageNumber", SqlDbType.VarChar).Value = page.DocumentPageNumber;
                        cmd.Parameters.Add("@PageBinary", SqlDbType.VarBinary).Value = page.PageBinaryData;

                        cmd.Parameters.Add("@DocumentImageId", SqlDbType.Int);
                        cmd.Parameters["@DocumentImageId"].Direction = ParameterDirection.Output;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        pageImageIds.Add(Convert.ToInt32(cmd.Parameters["@DocumentImageId"].Value));
                        conn.Close();
                    }
                }
            }
            return pageImageIds;
        }

        internal byte[] GetPageImageForSectionExtraction(int pageImageId)
        {
            DataTable pageImageContents = new DataTable();
            byte[] imageByte = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GetPageImageById";
                    cmd.Parameters.Add("@PageImageId", SqlDbType.Int).Value = pageImageId;
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    pageImageContents.Load(reader);
                    conn.Close();
                }
            }
            if (pageImageContents.Rows.Count > 0)
            {
                imageByte = (byte[])pageImageContents.Rows[0]["PageBinary"];
            }
            return imageByte;
        }
        #endregion
    }
}