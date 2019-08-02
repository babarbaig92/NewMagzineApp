using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewMagzineApp.AppCode.DTO
{
    public class PageImage
    {
        public int DocumentImageId { get; set; }
        public string DocumentImageName { get; set; }
        public int OriginalDocumentId { get; set; }
        public int DocumentPageNumber { get; set; }
        public DateTime PageCreationDate { get; set; }
        public byte[] PageBinaryData { get; set; }
        public DateTime OriginalDocumentUploadDate { get; set; }
    }
}