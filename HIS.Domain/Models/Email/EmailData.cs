using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Domain.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace HIS.Domain.Models.Email
{
    public class EmailData: CommonFields
    {
        [Key]
        public Int32 EmailId { get; set; }
        public string ScheduleDateTime { get; set; }
        public Nullable<bool> IsSent { get; set; }
        public String ToAddress { get; set; }
        public String CcAddress { get; set; }
        public String Subject { get; set; }
        public string EmailBody { get; set; }
        public string dSentDatetTime { get; set; }
        public Nullable<Int32> OrganizationId { get; set; }
        public int Priority { get; set; }

    }
    public class SmsData :CommonFields
    {
        [Key]
        public Int32 SmsId { get; set; }
        public string ScheduleDateTime { get; set; }
        public Nullable<bool> IsSent { get; set; }
        public String ToAddress { get; set; }
        public String CcAddress { get; set; }
        public String Subject { get; set; }
        public string SmsBody { get; set; }
        public string dSentDatetTime { get; set; }
        public Nullable<Int32> OrganizationId { get; set; }
        public int Priority { get; set; }
    }

}
