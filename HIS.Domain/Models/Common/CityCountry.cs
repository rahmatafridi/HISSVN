using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Domain.Models.Common
{
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string CityShortName { get; set; }
        public int CountryId { get; set; }

    }

    public class Country
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryShortName { get; set; }
    }
}
