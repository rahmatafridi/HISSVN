using HIS.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Domain.Models.Organization
{
    public class OrganizationLocation : CommonFields
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public int LocationType { get; set; }
        public string LocationTypeName { get; set; }
        public int OrganizationId { get; set; }

    }

    public class LocationType
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }
}
