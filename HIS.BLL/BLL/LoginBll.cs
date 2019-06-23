using HIS.DAL.DAL;
using HIS.Domain.Models.Common;
using HIS.Domain.Models.Module;
using HIS.Domain.Models.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.BLL
{
    public class LoginBll : ILoginBll
    {
        #region Initialization

        private ILoginDAL LoginDal { get; set; }

        public LoginBll()
        {
            LoginDal = new LoginDAL();
        }

        #endregion

        #region Change Password

        public bool DoesOldPasswordExists(int? userId, string oldPassword)
        {
            return LoginDal.DoesOldPasswordExists(userId, oldPassword);
        }

        public bool ChangePassword(int userId, string newPassword)
        {
            return LoginDal.ChangePassword(userId, newPassword);
        }


        #endregion



    }

    public interface ILoginBll
    {
        #region Change Password  

        bool DoesOldPasswordExists(int? userId, string oldPassword);

        bool ChangePassword(int userId, string newPassword);

        #endregion




    }
}
