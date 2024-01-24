namespace ICM_MUHASEBE_RAPOR_MİKRO.Tables
{
    public class STOK_HAREKETLERI
    {
        public Guid sth_Guid {  get; set; }
        public Guid sth_sip_uid {  get; set; }
        public Guid sth_fat_uid { get; set; }
        public string sth_stok_kod {  get; set; }
        public double sth_miktar {  get; set; }
        public double sth_tutar {  get; set; }
    }
}
