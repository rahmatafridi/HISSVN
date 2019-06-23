using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Domain.Models.Employee
{
   public class Employee
    {
        
        public int E_id { get; set; }
        public string E_name { get; set; }
        public string E_contact { get; set; }
        public string E_address { get; set; }



    }
}
