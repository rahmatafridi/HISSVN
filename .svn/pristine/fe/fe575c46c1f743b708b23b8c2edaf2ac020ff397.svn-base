using HIS.DAL.DbHelper;
using HIS.Domain.Models.Common;
using HIS.Domain.Models.Patient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.DAL.DAL
{
    public class PatientDAL : IPatientDAL
    {

        #region Initialization

        Database db;

        public PatientDAL()
        {
            db = new Database();
        }

        #endregion


        #region Patient

        public List<Patient> GetPatients(SearchCriteria criteria, out int TotalRecords)
        {
            List<Patient> roles = new List<Patient>();
            try
            {
                TotalRecords = 0;

                DataSet ds = new DataSet();

                List<DbParameter> param = new List<DbParameter>();
                param.Add(new DbParameter() { Name = "p_SearchText", Direction = ParameterDirection.Input, Value = criteria.SearchText, Type = DbType.String });
                param.Add(new DbParameter() { Name = "p_Offset", Direction = ParameterDirection.Input, Value = criteria.Offset, Type = DbType.Int32 });
                param.Add(new DbParameter() { Name = "p_PageSize", Direction = ParameterDirection.Input, Value = criteria.PageSize, Type = DbType.Int32 });


                ds = db.LoadDataSetAgainstQuery("pr_GetPatients", CommandType.StoredProcedure, ref param);

                if (ds != null && ds.Tables.Count > 0)
                {
                    roles = ds.Tables[0].AsEnumerable().Select(a => new Patient()
                    {
                        Id = Convert.ToInt32(a["Id"]),
                        FirstName = a["vFirstName"].ToString(),
                        LastName = a["vLastName"].ToString(),
                        MiddleName = a["vMiddleName"].ToString(),
                        DateOfBirth = Convert.ToDateTime(a["dDob"]),
                        TitleId = Convert.ToInt32(a["iTitleId"]),
                        GenderId = Convert.ToInt32(a["iGenderId"]),
                        CountryId = Convert.ToInt32(a["iCountryId"]),
                        BloodGroupId = Convert.ToInt32(a["iBloodGroup"]),
                        Mrno = a["vMrno"].ToString(),
                        ContactNumber = a["vContactNo"].ToString(),
                        HouseNo = a["vHouseNo"].ToString(),
                        StreetNo = a["vStreetNo"].ToString(),
                        Area = a["vArea"].ToString(),
                        EmergencyName = a["vEmergencyName"].ToString(),
                        EmergencyContact = a["vEmergencyContact"].ToString(),
                        State = a["vState"].ToString(),
                        UserId = Convert.ToInt32(a["iUserId"]),
                        IsActive = Convert.ToBoolean(a["bIsActive"]),
                        FatherHusbandName = a["vFatherHusbandName"].ToString(),
                        Email = a["vEmail"].ToString(),
                        PictureUrl = a["vPictureUrl"].ToString(),
                        SignatureUrl = a["vSignatureUrl"].ToString(),
                        EmergencyRelationId = Convert.ToInt32(a["iEmergencyRelation"]),
                        Cnic = a["vCNIC"].ToString(),
                        CreatedDate = Convert.ToDateTime(a["dCreatedDate"]),
                        UpdateDate = Convert.ToDateTime(a["dUpdateDate"]),
                        MaritalStatus = Convert.ToInt32(a["iMaritalStatus"]),
                        ReligionId = Convert.ToInt32(a["iReligion"]),
                        PreferredLanguage = Convert.ToInt32(a["iPreferredLanguage"]),
                        Nationality = a["vNationality"].ToString(),
                        MotherMaidenName = a["vMotherMaidenName"].ToString(),
                        EthnicGroup = Convert.ToInt32(a["iEthnicGroup"]),
                        BirthPlace = a["vBirthPlace"].ToString()

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

        public Patient GetPatientById(int id)
        {

            Patient Role = new Patient();
            try
            {

                DataSet ds = new DataSet();

                List<DbParameter> param = new List<DbParameter>();
                param.Add(new DbParameter() { Name = "p_PatId", Direction = ParameterDirection.Input, Value = id, Type = DbType.Int32 });


                ds = db.LoadDataSetAgainstQuery("pr_GetPatientById", CommandType.StoredProcedure, ref param);

                if (ds != null && ds.Tables.Count > 0)
                {
                    Role = ds.Tables[0].AsEnumerable().Select(a => new Patient()
                    {
                        Id = Convert.ToInt32(a["Id"]),
                        FirstName = a["vFirstName"].ToString(),
                        LastName = a["vLastName"].ToString(),
                        MiddleName = a["vMiddleName"].ToString(),
                        DateOfBirth = Convert.ToDateTime(a["dDob"]),
                        TitleId = Convert.ToInt32(a["iTitleId"]),
                        GenderId = Convert.ToInt32(a["iGenderId"]),
                        CountryId = Convert.ToInt32(a["iCountryId"]),
                        BloodGroupId = Convert.ToInt32(a["iBloodGroup"]),
                        Mrno = a["vMrno"].ToString(),
                        ContactNumber = a["vContactNo"].ToString(),
                        HouseNo = a["vHouseNo"].ToString(),
                        StreetNo = a["vStreetNo"].ToString(),
                        Area = a["vArea"].ToString(),
                        EmergencyName = a["vEmergencyName"].ToString(),
                        EmergencyContact = a["vEmergencyContact"].ToString(),
                        State = a["vState"].ToString(),
                        UserId = Convert.ToInt32(a["iUserId"]),
                        IsActive = Convert.ToBoolean(a["bIsActive"]),
                        FatherHusbandName = a["vFatherHusbandName"].ToString(),
                        Email = a["vEmail"].ToString(),
                        PictureUrl = a["vPictureUrl"].ToString(),
                        SignatureUrl = a["vSignatureUrl"].ToString(),
                        EmergencyRelationId = Convert.ToInt32(a["iEmergencyRelation"]),
                        Cnic = a["vCNIC"].ToString(),
                        CreatedDate = Convert.ToDateTime(a["dCreatedDate"]),
                        UpdateDate  =  Convert.ToDateTime(a["dUpdateDate"]),
                        MaritalStatus = Convert.ToInt32(a["iMaritalStatus"]),
                        ReligionId = Convert.ToInt32(a["iReligion"]),
                        PreferredLanguage = Convert.ToInt32(a["iPreferredLanguage"]),
                        Nationality = a["vNationality"].ToString(),
                        MotherMaidenName = a["vMotherMaidenName"].ToString(),
                        EthnicGroup = Convert.ToInt32(a["iEthnicGroup"]),
                        BirthPlace = a["vBirthPlace"].ToString()
                        
                    }).FirstOrDefault();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Role;


        }

        public int SavePatient(Patient patient)
        {
            int saved = 0;
            try
            {
                string query = "pr_CreateUpdatePatient";
                List<DbParameter> param = new List<DbParameter>();
                param.Add(new DbParameter() { Name = "p_PatId", Type = DbType.Int32, Value = patient.Id});
                param.Add(new DbParameter() { Name = "p_vFirstName", Type = DbType.String, Value = patient.FirstName });
                param.Add(new DbParameter() { Name = "p_vMiddleName", Type = DbType.String, Value = patient.MiddleName ?? "" });
                param.Add(new DbParameter() { Name = "p_vLastName", Type = DbType.String, Value = patient.LastName });
                param.Add(new DbParameter() { Name = "p_dDob", Type = DbType.Date, Value = patient.DateOfBirth });
                param.Add(new DbParameter() { Name = "p_iTitleId", Type = DbType.Int32, Value = patient.TitleId });
                param.Add(new DbParameter() { Name = "p_iGenderId", Type = DbType.Int32, Value = patient.GenderId });
                param.Add(new DbParameter() { Name = "p_iCountryId", Type = DbType.Int32, Value = patient.CountryId });
                param.Add(new DbParameter() { Name = "p_iBloodGroup", Type = DbType.Int32, Value = patient.BloodGroupId });
                param.Add(new DbParameter() { Name = "p_vMrno", Type = DbType.String, Value = patient.Mrno});
                param.Add(new DbParameter() { Name = "p_vContactNo", Type = DbType.String, Value = patient.ContactNumber ?? "" });
                param.Add(new DbParameter() { Name = "p_vHouseNo", Type = DbType.String, Value = patient.HouseNo ?? "" });
                param.Add(new DbParameter() { Name = "p_vStreetNo", Type = DbType.String, Value = patient.StreetNo ?? "" });
                param.Add(new DbParameter() { Name = "p_vArea", Type = DbType.String, Value = patient.Area ?? "" });
                param.Add(new DbParameter() { Name = "p_vEmergencyName", Type = DbType.String, Value = patient.EmergencyName ?? "" });
                param.Add(new DbParameter() { Name = "p_vEmergencyContact", Type = DbType.String, Value = patient.EmergencyContact ?? "" });
                param.Add(new DbParameter() { Name = "p_vState", Type = DbType.String, Value = patient.State ?? "" });
                param.Add(new DbParameter() { Name = "p_vPostalCode", Type = DbType.String, Value = patient.PostalCode ?? "" });
                param.Add(new DbParameter() { Name = "p_iUserId", Type = DbType.Int32, Value = patient.UserId });
                param.Add(new DbParameter() { Name = "p_vUserAccessCode", Type = DbType.String, Value = patient.AccessCode });
                param.Add(new DbParameter() { Name = "p_bIsActive", Type = DbType.Boolean, Value = patient.IsActive });
                param.Add(new DbParameter() { Name = "p_vFatherHusbandName", Type = DbType.String, Value = patient.FatherHusbandName ?? "" });
                param.Add(new DbParameter() { Name = "p_vEmail", Type = DbType.String, Value = patient.Email ?? "" });
                param.Add(new DbParameter() { Name = "p_vPictureUrl", Type = DbType.String, Value = patient.PictureUrl ?? ""});
                param.Add(new DbParameter() { Name = "p_vSignatureUrl", Type = DbType.String, Value = patient.SignatureUrl ?? "" });
                param.Add(new DbParameter() { Name = "p_iEmergencyRelation", Type = DbType.Int32, Value = patient.EmergencyRelationId });
                param.Add(new DbParameter() { Name = "p_vCNIC", Type = DbType.String, Value = patient.Cnic ?? "" });
                param.Add(new DbParameter() { Name = "p_iCreatedUserId", Type = DbType.Int32, Value = patient.CreatedUserId });
                param.Add(new DbParameter() { Name = "p_iMaritalStatus", Type = DbType.Int32, Value = patient.MaritalStatus });
                param.Add(new DbParameter() { Name = "p_iReligion", Type = DbType.Int32, Value = patient.ReligionId });
                param.Add(new DbParameter() { Name = "p_iPreferredLanguage", Type = DbType.Int32, Value = patient.PreferredLanguage });
                param.Add(new DbParameter() { Name = "p_vNationality", Type = DbType.Int32, Value = patient.Nationality });
                param.Add(new DbParameter() { Name = "p_vMotherMaidenName", Type = DbType.String, Value = patient.MotherMaidenName ?? "" });
                param.Add(new DbParameter() { Name = "p_iEthnicGroup", Type = DbType.Int32, Value = patient.EthnicGroup});
                param.Add(new DbParameter() { Name = "p_vBirthPlace", Type = DbType.String, Value = patient.BirthPlace ?? "" });


                saved = Convert.ToInt32(db.ExecuteScalar(query, CommandType.StoredProcedure, ref param));
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return saved;
        }

        public int NextPatientId
        {
            get
            {
                int saved = 0;
                try
                {
                    string query = "SELECT isnull(max(tp.Id),0) + 1 AS nextId FROM dbo.tblPatient tp;";
                    List<DbParameter> param = new List<DbParameter>();

                    saved = Convert.ToInt32(db.ExecuteScalar(query, CommandType.Text, ref param));
                }
                catch (Exception ex)
                {

                    throw ex;
                }

                return saved;
            }
        }


        #endregion

    }

    public interface IPatientDAL
    {
        #region Patient

        List<Patient> GetPatients(SearchCriteria criteria, out int TotalRecords);
        Patient GetPatientById(int id);
        int SavePatient(Patient patient);
        int NextPatientId { get; }
        #endregion


    }
}
