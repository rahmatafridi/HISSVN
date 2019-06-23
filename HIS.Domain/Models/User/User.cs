using HIS.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Domain.Models.User
{
    public class User : CommonFields
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public int TitleId { get; set; }
        public int Gender { get; set; }
        public string Password { get; set; }
        public string FatherHusbandName { get; set; }
        public bool FirstTimeLogin { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string CNIC { get; set; }
        public string PassportNo { get; set; }
        public string EmergencyContactNumber { get; set; }
        public string EmergencyContactPerson { get; set; }
        public string Designation { get; set; }
        public bool IsActive { get; set; }
        public int OrganizationId { get; set; }

        public string vUserImage { get; set; }
        
    }
}
