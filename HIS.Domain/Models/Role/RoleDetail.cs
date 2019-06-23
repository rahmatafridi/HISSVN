using HIS.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Domain.Models.Role
{
    public class RoleDetail: CommonFields
    {
        public Int32 iRoleDetailId { get; set; }
        public Nullable<Int32> iRoleId { get; set; }
        public Nullable<Int32> vModulePermissionId { get; set; }           
    }
}
