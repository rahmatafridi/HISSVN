using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Domain.Models.Common;

namespace HIS.Domain.Models.Module
{
    public class Module: CommonFields
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string ModuleCode { get; set; }
        public int OrganizationId { get; set; }
        public int ModulePermissionId { get; set; }
        public int IsCore { get; set; }
        public double ModulePrice { get; set; }
    }

    public class CoreModule
    {
        public int CoreModuleId { get; set; }
        public string CoreModuleText { get; set; }
    }
}
