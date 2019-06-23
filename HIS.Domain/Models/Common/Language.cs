using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Domain.Models.Common
{
    public class Language
    {
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
    }

    public class WorldTimeZone
    {
        public int WorldTimeZoneId { get; set; }
        public string WorldTimeZoneAbbrivation { get; set; }
        public string WorldTimeZoneName { get; set; }
        public string WorldTimeZoneOffSet { get; set; }
    }
}
