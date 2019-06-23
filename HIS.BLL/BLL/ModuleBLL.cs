using HIS.DAL.DAL;
using HIS.Domain.Models.Common;
using HIS.Domain.Models.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.BLL
{
    public class ModuleBLL: IModuleBll
    {
        #region Initialization

        private IModuleDAL ModuleDal { get; set; }

        public ModuleBLL()
        {
            ModuleDal = new ModuleDAL();
        }

        #endregion

        #region Module

        public List<Module> GetModule(SearchCriteria criteria, out int TotalRecords)
        {
            return ModuleDal.GetModule(criteria, out TotalRecords);
        }

        public Module GetModuleById(int id)
        {
            return ModuleDal.GetModuleById(id);
        }

        public int DeleteModuleById(int id)
        {
            return ModuleDal.DeleteModuleById(id);
        }

        public bool SaveModule(Module module)
        {
            return ModuleDal.SaveModule(module);
        }

        public List<CoreModule> GetCoreModuleDrpData()
        {
            return ModuleDal.GetCoreModuleDrpData();
        }

        #endregion

        #region Module Permission

        public List<ModulePermission> GetModulePermission(SearchCriteria criteria, out int TotalRecords)
        {
            return ModuleDal.GetModulePermission(criteria, out TotalRecords);
        }

        public ModulePermission GetModulePermissionById(int id)
        {
            return ModuleDal.GetModulePermissionById(id);
        }

        public int DeleteModulePermissionById(int id)
        {
            return ModuleDal.DeleteModulePermissionById(id);
        }

        public bool SaveModulePermission(ModulePermission modulePermission)
        {
            return ModuleDal.SaveModulePermission(modulePermission);
        }

        #endregion
    }

    public interface IModuleBll
    {
        #region Module

        List<Module> GetModule(SearchCriteria criteria, out int TotalRecords);

        Module GetModuleById(int id);

        int DeleteModuleById(int id);

        bool SaveModule(Module module);

        List<CoreModule> GetCoreModuleDrpData();

        #endregion

        #region Module Permission

        List<ModulePermission> GetModulePermission(SearchCriteria criteria, out int TotalRecords);

        ModulePermission GetModulePermissionById(int id);

        int DeleteModulePermissionById(int id);

        bool SaveModulePermission(ModulePermission modulePermission);

        #endregion
    }

}
