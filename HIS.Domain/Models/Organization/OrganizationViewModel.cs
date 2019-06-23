using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Domain.Models.Organization
{
    public class OrganizationViewModel
    {
        public Organization Organization { get; set; }
        public User.User User { get; set; }
    }
}
