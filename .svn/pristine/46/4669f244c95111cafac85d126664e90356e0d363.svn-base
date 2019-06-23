using HIS.DAL.DbHelper;
using HIS.Domain.Models.Common;
using HIS.Domain.Models.Module;
using HIS.Domain.Models.Organization;
using HIS.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIS.DAL.DAL
{
    public class LoginDAL : ILoginDAL
    {

        #region Initialization

        Database db;

        public LoginDAL()
        {
            db = new Database();
        }

        #endregion       

        #region Change Password

        public bool DoesOldPasswordExists(int? userId, string oldPassword)
        {
            bool exists = false;

            try
            {

                string query = "pr_GetOldPasswordExistsCount";

                List<DbParameter> param = new List<DbParameter>();
                param.Add(new DbParameter() { Name = "p_UserId", Value = userId });
                param.Add(new DbParameter() { Name = "p_OldPassword", Value = oldPassword });

                int saved = Convert.ToInt32(db.ExecuteScalar(query, CommandType.StoredProcedure, ref param));

                if (saved > 0)
                {
                    exists = true;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return exists;

        }

        public bool ChangePassword(int userId, string newPassword)
        {
            bool saved = false;
            try
            {
                string query = "pr_ChangePassword";
                List<DbParameter> param = new List<DbParameter>();
                param.Add(new DbParameter() { Name = "@p_UserId", Type = DbType.Int32, Value = userId, Length = 1000, Direction = ParameterDirection.Input });
                param.Add(new DbParameter() { Name = "@p_vNewPassword", Type = DbType.String, Value = newPassword, Length = 1000, Direction = ParameterDirection.Input });

                saved = db.ExecuteNonQuery(query, CommandType.StoredProcedure, ref param) > 0;



            }
            catch (Exception ex)
            {
                throw ex;
            }
            return saved;
        }

        #endregion



    }

    public interface ILoginDAL
    {
        #region Change Password     

        bool DoesOldPasswordExists(int? userId, string oldPassword);

        bool ChangePassword(int userId, string newPassword);

        #endregion

    }
}
