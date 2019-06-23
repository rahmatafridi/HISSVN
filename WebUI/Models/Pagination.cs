using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIS.Web.Models
{
    public class Pagination
    {
        public int Offset { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public string Url { get; set; }
    }
}