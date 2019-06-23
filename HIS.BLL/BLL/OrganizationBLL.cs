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
    public class OrganizationBLL : IOrganizationBLL
    {
        #region Initialization

        private IOrganizationDAL OrganizationDal { get; set; }

        public OrganizationBLL()
        {
            OrganizationDal = new OrganizationDAL();
        }

        #endregion

        #region Organization

        public List<Organization> GetOrganizations(SearchCriteria criteria, out int TotalRecords)
        {
            return OrganizationDal.GetOrganizations(criteria, out TotalRecords);
        }

        public Organization GetOrganizationById(int id)
        {
            return OrganizationDal.GetOrganizationById(id);
        }

        public int DeleteOrganizationById(int id)
        {
            return OrganizationDal.DeleteOrganizationById(id);
        }

        public int SaveOrganization(Organization Organization)
        {
            return OrganizationDal.SaveOrganization(Organization);
        }

        public bool DoesOrganizationExists(string email, int? organizationId = 0)
        {
            return OrganizationDal.DoesOrganizationExists(email, organizationId);
        }
        public bool SaveOrganizationModule(Organization organization, List<int> module)
        {
            return OrganizationDal.SaveOrganizationModule(organization, module);
        }

        public List<int> GetOrganizationModulesAssignedIds(int organizationId)
        {
            return OrganizationDal.GetOrganizationModulesAssignedIds(organizationId);
        }

        public List<OrganizationModule> GetOrganizationModulePermission(int organizationId)
        {
            return OrganizationDal.GetOrganizationModulePermission(organizationId);
        }

        public List<OrganizationModule> GetOrganizationModulePermissionDetailForRoles(int organizationId, int moduleId)
        {
            return OrganizationDal.GetOrganizationModulePermissionDetailForRoles(organizationId, moduleId);
        }

        #endregion

        #region Organization Status

        public List<OrganizationStatus> GetOrganizationStatus()
        {
            return OrganizationDal.GetOrganizationStatus();
        }

        #endregion

        #region Decline Organization

        public bool DeclineOrganization(int organizationId, string reason, int declineUserId)
        {
            return OrganizationDal.DeclineOrganization(organizationId, reason, declineUserId);
        }

        #endregion

        #region Approve Organization

        public bool ApproveOrganization(int organizationId, string activationDate, string expiryDate, string remarks, int approveUserId)
        {
            return OrganizationDal.ApproveOrganization(organizationId, activationDate, expiryDate, remarks, approveUserId);
        }

        #endregion

        #region Setup New Clinic

        public OrganizationViewModel GetDataForNewSetUpClinic(int organizationId, int userId)
        {
            return OrganizationDal.GetDataForNewSetUpClinic(organizationId, userId);
        }

        public bool UpdateClinicInformationForFirstTime(OrganizationViewModel model)
        {
            return OrganizationDal.UpdateClinicInformationForFirstTime(model);
        }

        
        #endregion

        #region Dashboard

        // country count organization wise

        public List<OrgStatChart> CountryWiseOrganization {
            get
            {
                return OrganizationDal.GetCountryWiseOrganizations();
            }
        }

        // user count organization wise

        public List<OrgStatChart> UserCountOrganizationWise
        {
            get
            {
                return OrganizationDal.GetUsersCountOrganizationWise();
            }
        }

        // module information organization wise

        public List<OrgStatChart> ModuleInformationOrganizatiionWise
        {
            get
            {
                return OrganizationDal.GetModuleInformationOrganizationWise();
            }
        }


        #endregion

        #region Organization Location

        public List<OrganizationLocation> GetLocationsAgainstOrganization(int orgId, SearchCriteria criteria, out int TotalRecords)
        {
            List<OrganizationLocation> locations = new List<OrganizationLocation>();

            locations = OrganizationDal.GetLocationsAgainstOrganization(orgId, criteria,out TotalRecords);

            return locations;
        }

        public int InsertUpdateOrganizationLocation(OrganizationLocation location)
        {
            int id = 0;

            id = OrganizationDal.InsertUpdateOrganizationLocation(location);

            return id;
        }

        public List<LocationType> LocationTypes
        {
            get
            {
                return OrganizationDal.GetLocationTypes();
            }        
        }

        public OrganizationLocation GetLocationsById(int orgId, int locationId)
        {
            return OrganizationDal.GetLocationsById(orgId, locationId);
        }

        #endregion
    }

    public interface IOrganizationBLL
    {
        #region Organization

        List<Organization> GetOrganizations(SearchCriteria criteria, out int TotalRecords);

        Organization GetOrganizationById(int id);

        int DeleteOrganizationById(int id);

        int SaveOrganization(Organization Organization);
        bool DoesOrganizationExists(string email, int? organizationId = 0);
        bool SaveOrganizationModule(Organization organization, List<int> module);
        List<int> GetOrganizationModulesAssignedIds(int organizationId);
        List<OrganizationModule> GetOrganizationModulePermission(int organizationId);
        List<OrganizationModule> GetOrganizationModulePermissionDetailForRoles(int organizationId, int moduleId);

        #endregion

        #region Organization Status

        List<OrganizationStatus> GetOrganizationStatus();

        #endregion

        #region Decline Organization

        bool DeclineOrganization(int organizationId, string reason, int declineUserId);

        #endregion

        #region Approve Organization
        bool ApproveOrganization(int organizationId, string activationDate, string expiryDate, string remarks, int approveUserId);
        #endregion

        #region Setup New Clinic
        OrganizationViewModel GetDataForNewSetUpClinic(int organizationId, int userId);
        bool UpdateClinicInformationForFirstTime(OrganizationViewModel model);

    
        #endregion

        #region Dashboard
        List<OrgStatChart> CountryWiseOrganization { get; }
        List<OrgStatChart> UserCountOrganizationWise { get; }
        List<OrgStatChart> ModuleInformationOrganizatiionWise { get; }
        #endregion

        #region Organization Location

        List<OrganizationLocation> GetLocationsAgainstOrganization(int orgId, SearchCriteria criteria, out int TotalRecords);
        int InsertUpdateOrganizationLocation(OrganizationLocation location);
        List<LocationType> LocationTypes { get; }
        OrganizationLocation GetLocationsById(int orgId, int locationId);

        #endregion
    }
}
