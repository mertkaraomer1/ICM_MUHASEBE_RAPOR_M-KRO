namespace ICM_MUHASEBE_RAPOR_MİKRO.Tables
{
    public class SIPARISLER
    {
        public Guid sip_Guid { get; set; }
        public int sip_DBCno { get; set; }
        public string sip_evrakno_seri { get; set; }
        public int sip_tip {  get; set; }
        public int sip_evrakno_sira { get; set; }
        public int sip_satirno { get; set; }
        public string sip_musteri_kod { get; set; }
        public string sip_aciklama { get; set; }
        public int sip_durumu { get; set; }
        public string sip_stok_kod { get; set; }
        public string sip_projekodu { get; set; }
        public double sip_miktar { get; set; }
        public double sip_teslim_miktar { get; set; }
        public DateTime sip_create_date { get; set; }
        public int sip_kapat_fl { get; set; }
        public DateTime sip_tarih { get; set; }
        public int sip_OnaylayanKulNo { get; set; }
        public string sip_stok_sormerk {  get; set; }
        public string sip_cari_sormerk { get; set; }

    }

}
