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
        [Description("EFatura ID")]
        public string EFatura_ID { get; set; }

        [Description("Belge tarihi")]
        public string Belge_tarihi  { get; set; }

        [Description("Cari ünvanı")]
        public string? Cari_ünvanı { get; set; }
        [Description("Yekün")]
        public string? Yekün { get; set; }

    }
}
