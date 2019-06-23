using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Domain.Models.Common;

namespace HIS.Domain.Models.Module
{
    public class OrganizationModule: CommonFields
    {
        public int ModuleId { get; set; }
        public int OrganizationId { get; set; }
        public string ModuleName { get; set; }
        public string ModuleCode { get; set; }
        public List<OrganizationModule> OrganizationModuleDetailList { get; set; }
        public int ModulePermissionId { get; set; }
        public string ModulePermissionCode { get; set; }
        public string ModulePermissionDescription { get; set; }

    }

    public class OrganizationModuleDetail
    {
        public int ModuleId { get; set; }
        public int ModulePermissionId { get; set; }
        public string ModulePermissionCode { get; set; }
        public string ModulePermissionDescription { get; set; }
    }
}
