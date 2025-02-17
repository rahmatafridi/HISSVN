﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.DAL.DAL;
using HIS.Domain.Models;
using HIS.Domain.Models.Common;

namespace HIS.BLL.BLL
{
    public class CommonBll : ICommonBll
    {
        #region Intialization
        private ICommonDal CommonDal { get; set; }
        public CommonBll()
        {
            CommonDal = new CommonDal();
        }
        #endregion

        #region Languages
        public List<Language> GetLanguages()
        {
            return CommonDal.GetLanguages();
        }

        #endregion

        #region TimeZone
        public List<WorldTimeZone> GetTimeZone()
        {
            return CommonDal.GetTimeZone();
        }

        #endregion

        #region Title
        public int InsertTitle(string title)
        {
            return CommonDal.InsertTitle(title);
        }

        public List<Title> Titles
        {
            get
            {
                return CommonDal.GetTitles();
            }
        }

        #endregion

        #region Country

        public List<Country> GetCountries()
        {
            return CommonDal.GetCountries();
        }
        public List<Country> Countries
        {
            get
            {
                return CommonDal.GetCountries();
            }
        }

        public int SaveCountry (Country country)
        {
            return CommonDal.SaveCountry(country);
        }
        #endregion

        #region City
        public List<City> Cities
        {
            get
            {
                return CommonDal.GetCities(1);
            }
        }
        #endregion

        #region Gender

        public List<Gender> Genders
        {
            get
            {
                return CommonDal.GetGenders();
            }
        }
        #endregion

        public List<KeyAndValue> MaritalStatus
        {
            get
            {
                return CommonDal.GetMaritalStatus();
            }

        }

        public List<KeyAndValue> Religion
        {
            get
            {
                return CommonDal.GetReligion();
            }

        }

        public List<KeyAndValue> Ethnicities
        {
            get
            {
                return CommonDal.GetEthnicities();
            }

        }
    }

    public interface ICommonBll
    {
        #region Languages

        List<Language> GetLanguages();

        #endregion   

        #region TimeZone

        List<WorldTimeZone> GetTimeZone();

        #endregion

        #region Title
        int InsertTitle(string title);
        List<Title> Titles { get; }
        #endregion

        #region Country
        List<Country> Countries { get; }
        List<Country> GetCountries();
        int SaveCountry(Country country);
        #endregion

        #region City
        List<City> Cities { get; }
        #endregion

        #region Gender

        List<Gender> Genders { get; }
        #endregion

        List<KeyAndValue> MaritalStatus { get; }

        List<KeyAndValue> Religion { get; }

        List<KeyAndValue> Ethnicities { get; }
    }
}
