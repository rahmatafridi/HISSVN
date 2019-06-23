using HIS.DAL.DbHelper;
using HIS.Domain.Models.Common;
using HIS.Domain.Models.Module;
using HIS.Domain.Models.Template;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.DAL.DAL
{
    public class TemplateDAL : ITemplateDAL
    {

        #region Initialization

        Database db;

        public TemplateDAL()
        {
            db = new Database();
        }

        #endregion

        #region Template

        public List<Template> GetTemplate(SearchCriteria criteria, out int TotalRecords)
        {
            List<Template> Templates = new List<Template>();
            try
            {

                TotalRecords = 0;

                DataSet ds = new DataSet();

                List<DbParameter> param = new List<DbParameter>();
                param.Add(new DbParameter() { Name = "p_SearchText", Direction = ParameterDirection.Input, Value = criteria.SearchText, Type = DbType.String });
                param.Add(new DbParameter() { Name = "p_Offset", Direction = ParameterDirection.Input, Value = criteria.Offset, Type = DbType.Int32 });
                param.Add(new DbParameter() { Name = "p_PageSize", Direction = ParameterDirection.Input, Value = criteria.PageSize, Type = DbType.Int32 });


                ds = db.LoadDataSetAgainstQuery("pr_GetTemplate", CommandType.StoredProcedure, ref param);

                if (ds != null && ds.Tables.Count > 0)
                {
                    Templates = ds.Tables[0].AsEnumerable().Select(a => new Template()
                    {
                        TemplateId = Convert.ToInt32(a["iTemplateId"]),
                        TemplateName = a["vTemplateName"].ToString(),
                        TemplateBody = a["tTemplateBody"].ToString(),
                        TemplateType = a["vTemplateType"].ToString(),
                        OrganizationId = a["iOrganizationId"] == DBNull.Value ? 0 : Convert.ToInt32(a["iOrganizationId"].ToString())
                        

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
            return Templates;
        }

        public Template GetTemplateById(int id)
        {

            Template template = new Template();
            try
            {

                DataSet ds = new DataSet();

                List<DbParameter> param = new List<DbParameter>();
                param.Add(new DbParameter() { Name = "p_TemplateId", Direction = ParameterDirection.Input, Value = id, Type = DbType.Int32 });


                ds = db.LoadDataSetAgainstQuery("pr_GetTemplateById", CommandType.StoredProcedure, ref param);

                if (ds != null && ds.Tables.Count > 0)
                {
                    template = ds.Tables[0].AsEnumerable().Select(a => new Template()
                    {
                        TemplateId = Convert.ToInt32(a["iTemplateId"]),
                        TemplateName = a["vTemplateName"].ToString(),
                        TemplateBody = a["tTemplateBody"].ToString(),
                        TemplateType = a["tTemplateBody"].ToString(),
                        OrganizationId = a["iOrganizationId"] == DBNull.Value ? 0 : Convert.ToInt32(a["iOrganizationId"].ToString())


                    }).FirstOrDefault();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return template;


        }

        public bool SaveTemplate(Template template)
        {
            try
            {

                string query = "pr_SaveTemplate";
                List<DbParameter> param = new List<DbParameter>();
                param.Add(new DbParameter() { Name = "p_TemplateId", Value = template.TemplateId });
                param.Add(new DbParameter() { Name = "p_TemplateName", Value = template.TemplateName });
                param.Add(new DbParameter() { Name = "p_TemplateBody", Value = template.TemplateBody });
                param.Add(new DbParameter() { Name = "p_TemplateType", Value = template.TemplateType });
                param.Add(new DbParameter() { Name = "p_OrganizationId", Value = template.OrganizationId });
                param.Add(new DbParameter() { Name = "p_CreatedUserId", Value = template.CreatedUserId });

                int result = db.ExecuteNonQuery(query, CommandType.StoredProcedure, ref param);


                return result > 0;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int DeleteTemplateById(int id)
        {
            int deleted = 0;
            try
            {

                string query = "pr_DeleteTemplateById";
                List<DbParameter> param = new List<DbParameter>();
                param.Add(new DbParameter() { Name = "@p_TemplateId", Value = id });

                deleted = db.ExecuteNonQuery(query, CommandType.StoredProcedure, ref param);

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return deleted;
        }

        public List<TemplateTypes> GetTemplateTypes()
        {
            List<TemplateTypes> templateType = new List<TemplateTypes>();
            templateType.Add(new TemplateTypes() { TypesId = 1, TypeText = "Email" });
            templateType.Add(new TemplateTypes() { TypesId = 2, TypeText = "Sms" });
            return templateType;
        }

        #endregion  

    }

    public interface ITemplateDAL
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
