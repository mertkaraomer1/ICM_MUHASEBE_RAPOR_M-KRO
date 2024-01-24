using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICM_MUHASEBE_RAPOR_MİKRO.Tables
{
    public class IHRACAT_DOSYALARI
    {
       public Guid ihr_Guid {  get; set; }
        public DateTime ihr_GCB_Tarihi {  get; set; }
        public DateTime ihr_create_date { get; set; }
        public string ihr_GCB_No {  get; set; }
        public byte ihr_DovizCinsi {  get; set; }
        public byte ihr_GCB_ETGB {  get; set; }
        public string ihr_Satici {  get; set; }
    }
}
