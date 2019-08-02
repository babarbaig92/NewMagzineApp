using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewMagzineApp.AppCode.DTO
{
    public class DocumentInfo
    {
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public DateTime UploadDate { get; set; }
    }
}