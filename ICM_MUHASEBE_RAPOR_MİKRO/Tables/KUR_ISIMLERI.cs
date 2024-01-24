using System.ComponentModel.DataAnnotations;

namespace ICM_MUHASEBE_RAPOR_MİKRO.Tables
{
    public class KUR_ISIMLERI
    {
        [Key]
        public Guid Kur_Guid { get; set; }
        public byte Kur_No {  get; set; }
        public string Kur_sembol { get; set; }
    }
}
