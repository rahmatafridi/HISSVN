using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.DAL.DbHelper;
using HIS.Domain.Models;
using HIS.Domain.Models.Common;

namespace HIS.DAL.DAL
{
    public class CommonDal : ICommonDal
    {
        #region Initialization
        Database db;

        public CommonDal()
        {
            db = new Database();
        }

        #endregion

        #region Languages

        public List<Language> GetLanguages()
        {
            List<Language> languages = new List<Language>();
            try
            {
                DataSet ds = new DataSet();
                List<DbParameter> param = new List<DbParameter>();

                ds = db.LoadDataSetAgainstQuery("Select iLanguagesId,vLanguageName from tblLanguages order by vLanguageName;", CommandType.Text, ref param);

                if (ds != null && ds.Tables.Count > 0)
                {
                    languages = ds.Tables[0].AsEnumerable().Select(a => new Language() { LanguageId = Convert.ToInt32(a["iLanguagesId"]), LanguageName = a["vLanguageName"].ToString() }).ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return languages;
        }

        #endregion

        #region TimeZone
        public List<WorldTimeZone> GetTimeZone()
        {
            List<WorldTimeZone> worldTimeZone = new List<WorldTimeZone>();
            try
            {
                DataSet ds = new DataSet();
                List<DbParameter> param = new List<DbParameter>();

                ds = db.LoadDataSetAgainstQuery("Select iWorldTimeZoneId,vWorldTimeZoneAbbr,vWorldTimeZoneName,dWorldTimeZoneOffset from tblWorldTimeZone;", CommandType.Text, ref param);

                if (ds != null && ds.Tables.Count > 0)
                {
                    worldTimeZone = ds.Tables[0].AsEnumerable().Select(a => new WorldTimeZone() { WorldTimeZoneId = Convert.ToInt32(a["iWorldTimeZoneId"]), WorldTimeZoneAbbrivation = a["vWorldTimeZoneAbbr"].ToString(), WorldTimeZoneName = a["vWorldTimeZoneName"].ToString(), WorldTimeZoneOffSet = a["dWorldTimeZoneOffset"].ToString() }).ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return worldTimeZone;
        }
        #endregion     

        #region Title
        public int InsertTitle(string title)
        {
            int result = 0;
            try
            {
                string query = "sp_InsertTitle";

                List<DbParameter> par = new List<DbParameter>();
                par.Add(new DbParameter() { Name = "p_TitleName", Value = title, Direction = System.Data.ParameterDirection.Input });

                result = db.ExecuteNonQuery(query, CommandType.StoredProcedure, ref par);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return result;
        }

        public List<Title> GetTitles()
        {
            List<Title> titles = new List<Title>();
            try
            {
                DataSet ds = new DataSet();
                List<DbParameter> param = new List<DbParameter>();

                ds = db.LoadDataSetAgainstQuery("SELECT iTitleId, vTitleName FROM tblTitle", CommandType.Text, ref param);

                if (ds != null && ds.Tables.Count > 0)
                {
                    titles = ds.Tables[0].AsEnumerable().Select(a => new Title() { TitleId = Convert.ToInt32(a["iTitleId"]), TitleName = a["vTitleName"].ToString() }).ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return titles;
        }
        #endregion

        #region Country
        public List<Country> GetCountries()
        {
            List<Country> titles = new List<Country>();
            try
            {
                DataSet ds = new DataSet();

                List<DbParameter> param = new List<DbParameter>();

                ds = db.LoadDataSetAgainstQuery("SELECT * FROM tblCountry", CommandType.Text, ref param);


                if (ds != null && ds.Tables.Count > 0)
                {
                    titles = ds.Tables[0].AsEnumerable().Select(a => new Country() { CountryId = Convert.ToInt32(a["iCountryId"]), CountryName = a["vCountryName"].ToString(), CountryShortName = a["vCountryShortName"].ToString() }).ToList();
                }

                titles.Insert(0, new Country() { CountryId = 0, CountryName = "--" });
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return titles;
        }

        public int SaveCountry(Country country)
        {
            int saved = 0;
            try
            {
                string query = "pr_SaveUpdateCountry";
                List<DbParameter> param = new List<DbParameter>();
                param.Add(new DbParameter() { Name = "p_iCountyid", Type = DbType.Int32, Value = country.CountryId });
                param.Add(new DbParameter() { Name = "p_vCountryShortName", Type = DbType.String, Value = country.CountryShortName });
                param.Add(new DbParameter() { Name = "p_vCountryName", Type = DbType.String, Value = country.CountryName });

                saved = Convert.ToInt32(db.ExecuteScalar(query, CommandType.StoredProcedure, ref param));
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return saved;
        }




        #endregion

        #region City

        public List<City> GetCities(int? countryId)
        {
            List<City> city = new List<City>();
            try
            {

                DataSet ds = new DataSet();
                List<DbParameter> param = new List<DbParameter>();

                ds = db.LoadDataSetAgainstQuery("SELECT * FROM tblCity", CommandType.Text, ref param);


                if (ds != null && ds.Tables.Count > 0)
                {
                    city = ds.Tables[0].AsEnumerable().Select(a => new City() { CityId = Convert.ToInt32(a["iCityId"]), CityName = a["vCityName"].ToString(), CityShortName = a["vCityShortName"].ToString() }).ToList();
                }

                city.Insert(0, new City() { CityId = 0, CityName = "--" });
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return city;
        }

        #endregion

        #region Gender

        public List<Gender> GetGenders()
        {
            List<Gender> titles = new List<Gender>();
            try
            {

                DataSet ds = new DataSet();
                List<DbParameter> param = new List<DbParameter>();

                ds = db.LoadDataSetAgainstQuery("SELECT * FROM tblGender", CommandType.Text, ref param);

                if (ds != null && ds.Tables.Count > 0)
                {
                    titles = ds.Tables[0].AsEnumerable().Select(a => new Gender() { GenderId = Convert.ToInt32(a["iGenderId"]), GenderName = a["vGenderName"].ToString() }).ToList();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return titles;
        }

        #endregion

        public List<KeyAndValue> GetMaritalStatus()
        {
            List<KeyAndValue> ms = new List<KeyAndValue>();

            try
            {

                DataSet ds = new DataSet();
                List<DbParameter> param = new List<DbParameter>();

                ds = db.LoadDataSetAgainstQuery("SELECT * FROM tblMaritalStatus", CommandType.Text, ref param);

                if (ds != null && ds.Tables.Count > 0)
                {
                    ms = ds.Tables[0].AsEnumerable().Select(a => new KeyAndValue() { Id = Convert.ToInt32(a["iMaritalStatusId"]), Value = a["vMaritalStatusName"].ToString() }).ToList();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return ms;
        }

        public List<KeyAndValue> GetReligion()
        {
            List<KeyAndValue> ms = new List<KeyAndValue>();

            try
            {

                DataSet ds = new DataSet();
                List<DbParameter> param = new List<DbParameter>();

                ds = db.LoadDataSetAgainstQuery("SELECT * FROM tblReligion", CommandType.Text, ref param);

                if (ds != null && ds.Tables.Count > 0)
                {
                    ms = ds.Tables[0].AsEnumerable().Select(a => new KeyAndValue() { Id = Convert.ToInt32(a["Id"]), Value = a["vReligion"].ToString() }).ToList();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return ms;
        }

        public List<KeyAndValue> GetEthnicities()
        {
            List<KeyAndValue> ms = new List<KeyAndValue>();

            try
            {

                DataSet ds = new DataSet();
                List<DbParameter> param = new List<DbParameter>();

                ds = db.LoadDataSetAgainstQuery("SELECT * FROM tblEthnicity", CommandType.Text, ref param);

                if (ds != null && ds.Tables.Count > 0)
                {
                    ms = ds.Tables[0].AsEnumerable().Select(a => new KeyAndValue() { Id = Convert.ToInt32(a["iEthnicityId"]), Value = a["vEthnicityName"].ToString() }).ToList();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return ms;
        }

    }

    public interface ICommonDal
    {
        #region Languages

        List<Language> GetLanguages();

        #endregion   

        #region TimeZone

        List<WorldTimeZone> GetTimeZone();

        #endregion

        #region Title
        int InsertTitle(string title);
        List<Title> GetTitles();
        #endregion

        #region Country
        List<Country> GetCountries();

        int SaveCountry(Country country);
        #endregion

        #region City
        List<City> GetCities(int? countryId);
        #endregion

        #region Gender
        List<Gender> GetGenders();
        #endregion       

        List<KeyAndValue> GetMaritalStatus();

        List<KeyAndValue> GetReligion();

        List<KeyAndValue> GetEthnicities();

    }
}
