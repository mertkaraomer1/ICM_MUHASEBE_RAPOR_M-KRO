using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICM_MUHASEBE_RAPOR_MİKRO.Tables
{
    public class CARI_HESAP_HAREKETLERI
    {
        public Guid cha_Guid {  get; set; }
        public Guid cha_uuid {  get; set; }
        public string cha_kod {  get; set; }
        public double cha_meblag {  get; set; }
        public byte cha_tip {  get; set; }
        public string cha_belge_no {  get; set; }
        public byte cha_evrak_tip { get; set; }
        public int cha_evrakno_sira { get; set; }
        public DateTime cha_belge_tarih { get; set; }
    }
}
