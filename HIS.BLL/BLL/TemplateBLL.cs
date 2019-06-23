using HIS.DAL.DAL;
using HIS.Domain.Models.Common;
using HIS.Domain.Models.Module;
using HIS.Domain.Models.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.BLL
{
    public class TemplateBLL: ITemplateBLL
    {
        #region Initialization

        private ITemplateDAL TemplateDal { get; set; }

        public TemplateBLL()
        {
            TemplateDal = new TemplateDAL();
        }

        #endregion

        #region Template

        public List<Template> GetTemplate(SearchCriteria criteria, out int TotalRecords)
        {
            return TemplateDal.GetTemplate(criteria, out TotalRecords);
        }

        public Template GetTemplateById(int id)
        {
            return TemplateDal.GetTemplateById(id);
        }

        public int DeleteTemplateById(int id)
        {
            return TemplateDal.DeleteTemplateById(id);
        }

        public bool SaveTemplate(Template template)
        {
            return TemplateDal.SaveTemplate(template);
        }

        public List<TemplateTypes> GetTemplateTypes()
        {
            return TemplateDal.GetTemplateTypes();
        }

        #endregion


    }

    public interface ITemplateBLL
    {
        #region Template

        List<Template> GetTemplate(SearchCriteria criteria, out int TotalRecords);

        Template GetTemplateById(int id);

        int DeleteTemplateById(int id);

        bool SaveTemplate(Template template);

        List<TemplateTypes> GetTemplateTypes();


        #endregion
    }

}
