using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Domain.Models.Common
{
    public class SearchCriteria
    {
        public int Offset { get; set; }
        public int PageSize { get; set; }
        public string SearchText { get; set; }

    }
}
