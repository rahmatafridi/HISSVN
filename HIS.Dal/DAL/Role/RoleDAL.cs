﻿using HIS.DAL.DbHelper;
using HIS.Domain.Models.Common;
using HIS.Domain.Models.Module;
using HIS.Domain.Models.Role;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.DAL.DAL
{
    public class RoleDAL : IRoleDAL
    {

        #region Initialization

        Database db;

        public RoleDAL()
        {
            db = new Database();
        }

        public RoleDAL(Database database)
        {
            db = database;
        }

        #endregion


        #region Role

        public List<Role> GetRoles(SearchCriteria criteria, out int TotalRecords)
        {
            List<Role> roles = new List<Role>();
            try
            {
                TotalRecords = 0;

                DataSet ds = new DataSet();

                List<DbParameter> param = new List<DbParameter>();
                param.Add(new DbParameter() { Name = "p_SearchText", Direction = ParameterDirection.Input, Value = criteria.SearchText, Type = DbType.String });
                param.Add(new DbParameter() { Name = "p_Offset", Direction = ParameterDirection.Input, Value = criteria.Offset, Type = DbType.Int32 });
                param.Add(new DbParameter() { Name = "p_PageSize", Direction = ParameterDirection.Input, Value = criteria.PageSize, Type = DbType.Int32 });


                ds = db.LoadDataSetAgainstQuery("pr_GetRoles", CommandType.StoredProcedure, ref param);

                if (ds != null && ds.Tables.Count > 0)
                {
                    roles = ds.Tables[0].AsEnumerable().Select(a => new Role()
                    {
                        iRoleId = Convert.ToInt32(a["iRoleId"].ToString()),
                        vRoleName = Convert.ToString(a["vRoleName"].ToString()),
                        vRoleDescription = Convert.ToString(a["vRoleDescription"].ToString()),
                        iOrganizationId = Convert.ToInt32(a["iOrganizationId"].ToString()),

                    }).ToList();

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        TotalRecords = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalCount"]);
                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return roles;
        }

        public Role GetRoleById(int id)
        {

            Role Role = new Role();
            try
            {

                DataSet ds = new DataSet();

                List<DbParameter> param = new List<DbParameter>();
                param.Add(new DbParameter() { Name = "p_RoleId", Direction = ParameterDirection.Input, Value = id, Type = DbType.Int32 });


                ds = db.LoadDataSetAgainstQuery("pr_GetRoleById", CommandType.StoredProcedure, ref param);

                if (ds != null && ds.Tables.Count > 0)
                {
                    Role = ds.Tables[0].AsEnumerable().Select(a => new Role()
                    {
                        iRoleId = Convert.ToInt32(a["iRoleId"].ToString()),
                        vRoleName = Convert.ToString(a["vRoleName"].ToString()),
                        vRoleDescription = Convert.ToString(a["vRoleDescription"].ToString()),
                        iOrganizationId = Convert.ToInt32(a["iOrganizationId"].ToString()),

                    }).FirstOrDefault();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Role;


        }

        public int SaveRole(Role Role)
        {
            int saved = 0;
            try
            {


                string query = "pr_SaveRole";
                List<DbParameter> param = new List<DbParameter>();
                param.Add(new DbParameter() { Name = "p_iRoleId", Type = DbType.Int32, Value = Role.iRoleId, Length = 1000, Direction = ParameterDirection.Input });
                param.Add(new DbParameter() { Name = "p_vRoleName", Type = DbType.String, Value = Role.vRoleName, Length = 1000, Direction = ParameterDirection.Input });
                param.Add(new DbParameter() { Name = "p_vRoleDescription", Type = DbType.String, Value = Role.vRoleDescription, Length = 1000, Direction = ParameterDirection.Input });
                param.Add(new DbParameter() { Name = "p_iOrganizationId", Type = DbType.Int32, Value = Role.iOrganizationId, Length = 1000, Direction = ParameterDirection.Input });
                param.Add(new DbParameter() { Name = "p_iCreatedUserId", Type = DbType.Int32, Value = Role.CreatedUserId, Length = 1000, Direction = ParameterDirection.Input });
                param.Add(new DbParameter() { Name = "p_dCreatedDate", Type = DbType.DateTime, Value = System.DateTime.Now, Length = 1000, Direction = ParameterDirection.Input });
                param.Add(new DbParameter() { Name = "p_iUpdatedUserId", Type = DbType.Int32, Value = Role.UpdateUserId, Length = 1000, Direction = ParameterDirection.Input });
                param.Add(new DbParameter() { Name = "p_dUpdatedDate", Type = DbType.DateTime, Value = Role.UpdateDate, Length = 1000, Direction = ParameterDirection.Input });


                saved = Convert.ToInt32(db.ExecuteScalar(query, CommandType.StoredProcedure, ref param));
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return saved;
        }

        public bool SaveRoleData(Role role, List<Module> modules)
        {
            bool saved = false;
            try
            {
                db.BeginTransaction();
                int roleId = SaveRole(role);
                if (roleId > 0)
                {
                    saved = SaveRoleModulePermission(roleId, modules);

                    db.Commit();
                }
                else
                {
                    db.RollBack();
                }
            }
            catch (Exception ex)
            {
                db.RollBack();
                //tran.Dispose();
                throw ex;
            }
            //  }

            //}

            return saved;
        }

        public int DeleteRoleById(int id)
        {
            try
            {

                bool delete = DeleteRoleModulePermission(id);
                int deleted = 0;
                if (delete)
                {
                    string query = "pr_DeleteRoleById";
                    List<DbParameter> param = new List<DbParameter>();
                    param.Add(new DbParameter() { Name = "p_Id", Value = id });

                    deleted = db.ExecuteNonQuery(query, CommandType.StoredProcedure, ref param);
                }

                return deleted;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool SaveUserRole(int userId, List<int> Role,int? createdUserId=0)
        {
            bool saved = false;
            try
            {

                DeleteUserRoles(userId);

                string query = "pr_SaveUserRole";

                foreach (int rol in Role)
                {
                    List<DbParameter> param = new List<DbParameter>();
                    param.Add(new DbParameter() { Name = "p_RoleId", Value = rol});
                    param.Add(new DbParameter() { Name = "p_UserId", Value = userId });
                    param.Add(new DbParameter() { Name = "p_CreatedUserId", Value = createdUserId });

                    saved = db.ExecuteNonQuery(query, CommandType.StoredProcedure, ref param) > 0;
                }



            }
            catch (Exception ex)
            {

                throw ex;
            }
            return saved;
        }

        private bool DeleteUserRoles(int userId)
        {
            try
            {

                string query = "pr_DeleteUserRoles";
                List<DbParameter> param = new List<DbParameter>();
                param.Add(new DbParameter() { Name = "p_UserId", Value = userId });

                int deleted = db.ExecuteNonQuery(query, CommandType.StoredProcedure, ref param);

                return deleted > 0;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<int> GetUserAssignedRoleIds(int userId)
        {
            List<int> ids = new List<int>();
            try
            {

                DataSet ds = new DataSet();

                List<DbParameter> param = new List<DbParameter>();
                param.Add(new DbParameter() { Name = "p_UserId", Direction = ParameterDirection.Input, Value = userId, Type = DbType.Int32 });


                ds = db.LoadDataSetAgainstQuery("pr_GetRolesAssignedToUser", CommandType.StoredProcedure, ref param);

                if (ds != null && ds.Tables.Count > 0)
                {
                    ids = ds.Tables[0].AsEnumerable().Select(a => Convert.ToInt32(a[0])).ToList();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return ids;
        }


        #endregion

        #region Role Module Permission

        public bool SaveRoleModulePermission(int roleId, List<Module> module)
        {
            bool saved = false;
            try
            {

                DeleteRoleModulePermission(roleId);

                string query = "pr_SaveModuleRolePermission";

                foreach (Module rol in module)
                {
                    List<DbParameter> param = new List<DbParameter>();
                    param.Add(new DbParameter() { Name = "p_ModuleId", Value = rol.ModuleId });
                    param.Add(new DbParameter() { Name = "p_RoleId", Value = roleId });
                    param.Add(new DbParameter() { Name = "p_ModulePermissionId", Value = rol.ModulePermissionId });
                    param.Add(new DbParameter() { Name = "p_CreatedUserId", Value = rol.CreatedUserId });

                    saved = db.ExecuteNonQuery(query, CommandType.StoredProcedure, ref param) > 0;
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }

            return saved;
        }

        private bool DeleteRoleModulePermission(int roleId)
        {
            try
            {

                string query = "pr_DeleteModuleRolePermissions";
                List<DbParameter> param = new List<DbParameter>();
                param.Add(new DbParameter() { Name = "p_RoleId", Value = roleId });

                int deleted = db.ExecuteNonQuery(query, CommandType.StoredProcedure, ref param);

                return deleted > 0;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<int> GetRoleModulesAssignedPermissionIds(int roleId)
        {
            List<int> ids = new List<int>();
            try
            {

                DataSet ds = new DataSet();

                List<DbParameter> param = new List<DbParameter>();
                param.Add(new DbParameter() { Name = "p_RoleId", Direction = ParameterDirection.Input, Value = roleId, Type = DbType.Int32 });


                ds = db.LoadDataSetAgainstQuery("pr_GetModulePermissionAssignedToRole", CommandType.StoredProcedure, ref param);

                if (ds != null && ds.Tables.Count > 0)
                {
                    ids = ds.Tables[0].AsEnumerable().Select(a => Convert.ToInt32(a[0])).ToList();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return ids;
        }
        #endregion


    }

    public interface IRoleDAL
    {
        #region Role

        List<Role> GetRoles(SearchCriteria criteria, out int TotalRecords);
        Role GetRoleById(int id);
        int DeleteRoleById(int id);
        int SaveRole(Role Role);
        bool SaveUserRole(int userId, List<int> Role, int? createdUserId = 0);
        List<int> GetUserAssignedRoleIds(int userId);
        bool SaveRoleData(Role role, List<Module> modules);

        #endregion

        #region Role Module Permission

        bool SaveRoleModulePermission(int roleId, List<Module> module);

        List<int> GetRoleModulesAssignedPermissionIds(int roleId);

        #endregion


    }
}
