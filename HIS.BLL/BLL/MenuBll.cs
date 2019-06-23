using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Domain.Models.Menu;
using HIS.DAL;
using HIS.Domain.Models.Common;

namespace HIS.BLL.BLL
{
    public class MenuBll : IMenuBll
    {

        #region Initialization

        private IMenuDAL MenuDal { get; set; }

        public MenuBll()
        {
            MenuDal = new MenuDAL();
        }
        #endregion

        #region User Menu
        public List<Menu> GetUserMenus(int userId, int organizationId)
        {
            return MenuDal.GetUserMenus(userId, organizationId);
        }

        #endregion

        #region Menus

        public List<Menu> GetMenus(SearchCriteria criteria, out int TotalRecords)
        {
            return MenuDal.GetMenus(criteria, out TotalRecords);
        }

        public Menu GetMenuById(int id)
        {
            return MenuDal.GetMenuById(id);
        }

        public List<Menu> GetAllMenus()
        {
            return MenuDal.GetAllMenus();
        }
        public int SaveMenu(Menu menu)
        {
            return MenuDal.SaveMenu(menu);
        }
        public bool DeleteMenuById(int id)
        {
            return MenuDal.DeleteMenuById(id);
        }

        #endregion
    }

    public interface IMenuBll
    {
        #region User Menu
        List<Menu> GetUserMenus(int userId, int organizationId);

        #endregion

        #region Menus
        List<Menu> GetMenus(SearchCriteria criteria, out int TotalRecords);
        Menu GetMenuById(int id);
        List<Menu> GetAllMenus();
        int SaveMenu(Menu menu);
        bool DeleteMenuById(int id);
        #endregion
    }
}
