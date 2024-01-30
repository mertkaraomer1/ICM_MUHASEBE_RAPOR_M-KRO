using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICM_MUHASEBE_RAPOR_MİKRO.Lists
{
    public class Faturalar
    {
        [Description("TARİH")]
        public string TARİH { get; set; }

        [Description("BELGE TÜRÜ")]
        public string BELGE_TÜRÜ  { get; set; }

        [Description("BELGE NO")]
        public string? BELGE_NO { get; set; }

    }
}
