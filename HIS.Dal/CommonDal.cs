using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Dal
{
    public class CommonDal
    {
        Database db;

        public CommonDal()
        {
            db = new Database();
        }

        public int InsertTitle(string Title)
        {
            int result = 0;

            string query = "sp_InsertTitle";

            List<DbParameter> par = new List<DbParameter>();
            par.Add(new DbParameter() { Name="p_TitleName", Value = Title, Direction = System.Data.ParameterDirection.Input });

            result = db.ExecuteNonQuery(query, System.Data.CommandType.StoredProcedure, par);


            return result;
        }

        public List<string> GetTitles()
        {
            List<string> titles = new List<string>();

            DataSet ds = new DataSet();

            ds = db.LoadDataSetAgainstQuery("SELECT OID, TITLE FROM TITLE", CommandType.Text, null);

            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    titles.Add(row["title"].ToString());
                }
            }

            return titles;

        }
    }
}
