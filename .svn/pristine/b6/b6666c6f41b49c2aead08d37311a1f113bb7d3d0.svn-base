using HIS.DAL.DAL;
using HIS.Domain.Models.Common;
using HIS.Domain.Models.Module;
using HIS.Domain.Models.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.BLL
{
    public class RoleBLL : IRoleBLL
    {
        #region Initialization
        IRoleDAL _role;

        public RoleBLL()
        {
            _role = new RoleDAL();
        }
        #endregion

        #region Role
        public List<Role> GetRoles(SearchCriteria criteria, out int TotalRecords)
        {
            return _role.GetRoles(criteria, out TotalRecords);
        }

        public bool SaveUserRole(int userId, List<int> Role)
        {
            return _role.SaveUserRole(userId, Role);
        }

        public List<int> GetUserAssignedRoleIds(int userId)
        {
            return _role.GetUserAssignedRoleIds(userId);
        }
        public Role GetRoleById(int id)
        {
            return _role.GetRoleById(id);
        }

        public int SaveRole(Role Role)
        {
            return _role.SaveRole(Role);
        }
        public int DeleteRoleById(int id)
        {
            return _role.DeleteRoleById(id);
        }

        public bool SaveRoleData(Role role, List<Module> modules)
        {
            return _role.SaveRoleData(role, modules);
        }

            #endregion

            #region Role Module Permission

            public bool SaveRoleModulePermission(int roleId, List<Module> module)
        {
            return _role.SaveRoleModulePermission(roleId, module);
        }
        public List<int> GetRoleModulesAssignedPermissionIds(int roleId)
        {
            return _role.GetRoleModulesAssignedPermissionIds(roleId);
        }
        #endregion
    }

    public interface IRoleBLL
    {
        #region Role
        List<Role> GetRoles(SearchCriteria criteria, out int TotalRecords);
        bool SaveUserRole(int userId, List<int> Role);
        List<int> GetUserAssignedRoleIds(int userId);
        Role GetRoleById(int id);
        int SaveRole(Role Role);
        int DeleteRoleById(int id);
        #endregion

        #region  Role Module Permission
        bool SaveRoleModulePermission(int roleId, List<Module> module);

        List<int> GetRoleModulesAssignedPermissionIds(int roleId);

        bool SaveRoleData(Role role, List<Module> modules);
        

            #endregion
        }

}
