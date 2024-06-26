﻿using ICM_MUHASEBE_RAPOR_MİKRO.Context;
using ICM_MUHASEBE_RAPOR_MİKRO.Lists;
using Microsoft.Office.Interop.Excel;
using System.Data;
using System.Data.OleDb;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ICM_MUHASEBE_RAPOR_MİKRO
{
    public partial class IHRACAT_RAPOR : Form
    {
        private KDBContext KDBContext;
        private MyDbContext dbContext;
        public IHRACAT_RAPOR()
        {
            KDBContext = new KDBContext();
            dbContext = new MyDbContext();
            InitializeComponent();
        }
        System.Data.DataTable table = new System.Data.DataTable();
        public void IHRACAT_RAPOR_Load(object sender, EventArgs e)
        {
            SetCircularRegion(button1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selectedDate1 = dateTimePicker2.Value.Date;
            var selectedDate = dateTimePicker1.Value.Date;

            table.Clear();
            table.Columns.Clear();
            table.Rows.Clear();

            table.Columns.Add("SATIR_NO");
            table.Columns.Add("GCB_NO");
            table.Columns.Add("GCB_TARİH");
            table.Columns.Add("BELGE NO");
            table.Columns.Add("DOVİZ CİNSİ");
            table.Columns.Add("GCB_ETGB");
            table.Columns.Add("CARİ_ADİ");
            table.Columns.Add("GTİPKODU");
            table.Columns.Add("MİKTAR");
            table.Columns.Add("BİRİM");
            table.Columns.Add("TUTAR");
            table.Columns.Add("DİP TOPLAM");

            var result = dbContext.STOK_HAREKETLERI
                .Join(
                    dbContext.STOKLAR,
                    stok => stok.sth_stok_kod,
                    cari => cari.sto_kod,
                    (stok, cari) => new { Stok = stok, Cari = cari }
                ).Select(x => new
                {
                    x.Stok.sth_fat_uid,
                    x.Stok.sth_miktar,
                    x.Cari.sto_GtipNo,
                    x.Cari.sto_birim1_ad,
                    x.Stok.sth_tutar
                })
                .Join(
                    dbContext.CARI_HESAP_HAREKETLERI,
                    stok => stok.sth_fat_uid,
                    cari => cari.cha_Guid,
                    (stok, cari) => new { Stok = stok, Cari = cari }
                ).Where(x =>
            x.Cari.cha_belge_tarih >= selectedDate && x.Cari.cha_belge_tarih <= selectedDate1)
                .Select(x => new
                {
                    x.Stok.sth_fat_uid,
                    x.Stok.sth_miktar,
                    x.Stok.sto_GtipNo,
                    x.Stok.sto_birim1_ad,
                    x.Stok.sth_tutar,
                    x.Cari.cha_meblag,
                    x.Cari.cha_kod,
                    x.Cari.cha_belge_no
                })
                .Join(
                    dbContext.CARI_HESAPLAR,
                    joinedTables => joinedTables.cha_kod,
                    cari => cari.cari_kod,
                    (joinedTables, cari) => new { StokCari = joinedTables, CariBilgi = cari }
                )
                .Select(result => new
                {
                    carikod = result.CariBilgi.cari_kod,
                    CariUnvan = result.CariBilgi.cari_unvan1,
                    gtipkod = result.StokCari.sto_GtipNo,
                    miktar = result.StokCari.sth_miktar,
                    Birim = result.StokCari.sto_birim1_ad,
                    Tutar = result.StokCari.sth_tutar,
                    meblag = result.StokCari.cha_meblag,
                    BelgeNo = result.StokCari.cha_belge_no
                })
                .ToList();


            var ihracatraporu = dbContext.IHRACAT_DOSYALARI
                .Where(x =>
            x.ihr_create_date >= selectedDate && x.ihr_create_date <= selectedDate1)
                .AsEnumerable() // Bring data into memory
                .Join(result,
                    stok => stok.ihr_Satici,
                    cari => cari.carikod,
                    (stok, cari) => new { Stok = stok, Cari = cari })
                .Select(x => new
                {
                    GCB_NO = x.Stok.ihr_GCB_No,
                    TARİH = x.Stok.ihr_GCB_Tarihi,
                    Doviz_Cinsi = KDBContext.KUR_ISIMLERI.Where(y => y.Kur_No == x.Stok.ihr_DovizCinsi).FirstOrDefault().Kur_sembol,
                    GCB_ETGB = x.Stok.ihr_GCB_ETGB == 0 ? "GCB" : "ETGB",
                    CARİ_ADİ = x.Cari.CariUnvan,
                    GTİPKODU = x.Cari.gtipkod,
                    MİKTAR = x.Cari.miktar,
                    BİRİM = x.Cari.Birim,
                    TUTAR = x.Cari.Tutar,
                    DİP_TOPLAM = x.Cari.meblag,
                    BELGE_NO = x.Cari.BelgeNo
                })
                .ToList();
            // DataGridView başlık satırının görünümünü ayarlayın
            advancedDataGridView1.EnableHeadersVisualStyles = false;
            advancedDataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            advancedDataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // Özel bir yazı tipi tanımlayın
            System.Drawing.Font baslikYaziTipi = new System.Drawing.Font("Arial", 10, FontStyle.Regular);

            // DataGridView başlık satırının yazı tipini ayarlayın
            advancedDataGridView1.ColumnHeadersDefaultCellStyle.Font = baslikYaziTipi;

            int sayac = 1;
            foreach (var urun in ihracatraporu)
            {
                table.Rows.Add(sayac++, urun.GCB_NO, urun.TARİH, urun.BELGE_NO, urun.Doviz_Cinsi, urun.GCB_ETGB, urun.CARİ_ADİ, urun.GTİPKODU, urun.MİKTAR, urun.BİRİM, urun.TUTAR, urun.DİP_TOPLAM);
            }
            advancedDataGridView1.DataSource = table;
        }
        private void SetCircularRegion(System.Windows.Forms.Button button)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, button.Width, button.Height);
            button.Region = new Region(path);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // DataGridView'deki verileri bir DataTable'a kopyalayın
            System.Data.DataTable dt = new System.Data.DataTable();

            foreach (DataGridViewColumn column in advancedDataGridView1.Columns)
            {
                // Eğer ValueType null ise, varsayılan bir veri türü kullanabilirsiniz.
                Type columnType = column.ValueType ?? typeof(string);
                dt.Columns.Add(column.HeaderText, columnType);
            }

            // Satırları ekle
            foreach (DataGridViewRow row in advancedDataGridView1.Rows)
            {
                DataRow dataRow = dt.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null) // Hücre değeri null değilse
                    {
                        dataRow[cell.ColumnIndex] = cell.Value;
                    }
                    else if (cell is DataGridViewCheckBoxCell) // CheckBox hücresi ise
                    {
                        dataRow[cell.ColumnIndex] = (cell.Value != null && (bool)cell.Value) ? "True" : "False"; // CheckBox değeri null değilse ve true ise "True" olarak ayarla, değilse "False" olarak ayarla
                    }
                }
                dt.Rows.Add(dataRow);
            }

            // Excel uygulamasını başlatın
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelApp.Visible = true;

            // Yeni bir Excel çalışma kitabı oluşturun
            Workbook workbook = excelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            _Worksheet worksheet = (_Worksheet)workbook.Worksheets[1];

            // DataTable'ı Excel çalışma sayfasına aktarın
            int rowIndex = 1;

            // Başlıkları yaz
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                worksheet.Cells[1, j + 1] = dt.Columns[j].ColumnName;
                worksheet.Cells[1, j + 1].Font.Bold = true;
            }

            // Verileri yaz
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rowIndex++;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    // Metin olarak aktar
                    worksheet.Cells[rowIndex, j + 1].NumberFormat = "@";
                    worksheet.Cells[rowIndex, j + 1] = dt.Rows[i][j].ToString();
                }
            }
        }



    }
}
