using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Dal
{
    public class DbHelper
    {
        Database db;

        public DbHelper()
        { 
            
        }
    }

    public class Database
    {
        static SqlConnection connection { get; set; }
        SqlCommand command { get; set; }


        public Database()
        {
            CreateDatabase();
        }
        /// <summary>
        /// Create connection with the "DefaultConnection" connection string
        /// </summary>
        public void CreateDatabase()
        {
            if (connection == null || connection.State != System.Data.ConnectionState.Open)
            {
                string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

                connection = new SqlConnection(ConnectionString);
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

        public void CreateDatabase(string ConnectionStringName)
        {
            if (connection == null || connection.State != System.Data.ConnectionState.Open)
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings[ConnectionStringName].ToString());

                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }


        public DataSet LoadDataSetAgainstQuery(string Query, System.Data.CommandType Type, List<DbParameter> Parameters)
        {
            DataSet ds = new DataSet();

            try
            {
                
                command = connection.CreateCommand();

                command.CommandText = Query;
                command.CommandType = Type;

                if (Parameters != null && Parameters.Count > 0)
                {
                    foreach (DbParameter param in Parameters)
                    {
                        command.Parameters.AddWithValue(param.Name, param.Value);
                        command.Parameters[param.Name].DbType = param.Type;
                        command.Parameters[param.Name].Direction = param.Direction;

                    }
                }

                


                SqlDataAdapter da = new SqlDataAdapter(command);

                da.Fill(ds);

                command.Dispose();
            }
            catch (Exception ex)
            {

                throw;
            }

            

            return ds;
           
        }

        public int ExecuteNonQuery(string Query, System.Data.CommandType Type, List<DbParameter> Parameters)
        {
            int result = 0;

            try
            {
                command = connection.CreateCommand();

                command.CommandText = Query;
                command.CommandType = Type;

                if (Parameters != null && Parameters.Count > 0)
                {
                    foreach (DbParameter param in Parameters)
                    {
                        command.Parameters.AddWithValue(param.Name, param.Value);
                        command.Parameters[param.Name].DbType = param.Type;
                        command.Parameters[param.Name].Direction = param.Direction;

                    }
                }

                result = command.ExecuteNonQuery();
                
                command.Dispose();

            }
            catch (Exception ex)
            {

                throw;
            }

            

            return result;
        }

        public object ExecuteScalar(string Query, System.Data.CommandType Type, List<DbParameter> Parameters)
        {
            object result = null;

            try
            {
                command = connection.CreateCommand();

                command.CommandText = Query;
                command.CommandType = Type;

                if (Parameters != null && Parameters.Count > 0)
                {
                    foreach (DbParameter param in Parameters)
                    {
                        command.Parameters.AddWithValue(param.Name, param.Value);
                        command.Parameters[param.Name].DbType = param.Type;
                        command.Parameters[param.Name].Direction = param.Direction;

                    }
                }

                result = command.ExecuteScalar();

                command.Dispose();
            }
            catch (Exception ex)
            {

                throw;
            }
            
            return result;
        }


    }

    public class DbParameter
    {
        public string Name { get; set; }
        public DbType Type { get; set; }
        public ParameterDirection Direction { get; set; }
        public int Length { get; set; }
        public object Value { get; set; }
    }

}
