﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICM_MUHASEBE_RAPOR_MİKRO.Lists;
using System.Data.OleDb;

namespace ICM_MUHASEBE_RAPOR_MİKRO.Context
{
    public partial class FİŞ_EŞLEŞTİRME : Form
    {
        private MyDbContext dbContext;
        public FİŞ_EŞLEŞTİRME()
        {
            dbContext = new MyDbContext();
            InitializeComponent();
        }
        DataTable table = new DataTable();
        // Manually map DataTable to List<Faturalar>
        List<Faturalar> FAAturaList = new List<Faturalar>();
        private void button2_Click(object sender, EventArgs e)
        {
            var selectedDate1 = dateTimePicker2.Value.Date;
            var selectedDate = dateTimePicker1.Value.Date;
            table.Clear();
            table.Columns.Clear();
            table.Rows.Clear();

            table.Columns.Add("SATIR_NO");
            table.Columns.Add("TİCARET BELGE NO");
            table.Columns.Add("FİŞ TARİH");
            table.Columns.Add("BELGE TİPİ");
            table.Columns.Add("BELGE NO");


            var result = dbContext.MUHASEBE_FISLERI
                .Where(x => x.fis_tarih >= selectedDate && x.fis_tarih < selectedDate1)
                .Join(
                    dbContext.CARI_HESAP_HAREKETLERI,
                    fis => fis.fis_tic_belgeno,
                    cha => cha.cha_belge_no,
                    (fis, cha) => new { Fis = fis, Cha = cha }
                )
                .Where(joined => joined.Cha.cha_evrak_tip == 0 && joined.Cha.cha_belge_no != "")
                .Select(joined => new
                {
                    joined.Fis.fis_tic_belgeno,
                    joined.Fis.fis_tarih,
                    joined.Cha.cha_evrak_tip
                }).Distinct()
                .ToList();



            // DataGridView başlık satırının görünümünü ayarlayın
            advancedDataGridView1.EnableHeadersVisualStyles = false;
            advancedDataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            advancedDataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // Özel bir yazı tipi tanımlayın
            Font baslikYaziTipi = new Font("Arial", 10, FontStyle.Regular);

            // DataGridView başlık satırının yazı tipini ayarlayın
            advancedDataGridView1.ColumnHeadersDefaultCellStyle.Font = baslikYaziTipi;
            int sayac = 1;
            foreach (var urun in result)
            {
                var matchedProject = FAAturaList.FirstOrDefault(sip => sip.BELGE_NO == urun.fis_tic_belgeno);

                table.Rows.Add(sayac++, urun.fis_tic_belgeno, (matchedProject != null) ? matchedProject.TARİH:"YOK", (matchedProject != null) ? matchedProject.BELGE_TÜRÜ : "YOK", (matchedProject != null) ? matchedProject.BELGE_NO : "YOK");
            }

            advancedDataGridView1.DataSource = table;
            for (int i = 0; i < advancedDataGridView1.Rows.Count; i++)
            {
                DataGridViewRow row = advancedDataGridView1.Rows[i];
                string belgeNo = row.Cells[4].Value.ToString();

                // Eşleşen satırları yeşil yap
                if (belgeNo == "YOK")
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.Style.BackColor = Color.Red;
                    }
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Dosya seçme penceresi açmak için
                OpenFileDialog file = new OpenFileDialog();
                file.Filter = "Excel Dosyası |*.xlsx";
                file.ShowDialog();

                // seçtiğimiz excel'in tam yolu
                string tamYol = file.FileName;

                // Excel bağlantı adresi
                string baglantiAdresi = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + tamYol + ";Extended Properties='Excel 12.0;IMEX=1;'";

                // bağlantı 
                using (OleDbConnection baglanti = new OleDbConnection(baglantiAdresi))
                {
                    // tüm verileri seçmek için select sorgumuz. Sayfa1 olan kısmı Excel'de hangi sayfayı açmak istiyorsanız orayı yazabilirsiniz.
                    OleDbCommand komut = new OleDbCommand("Select * From [" + textBox1.Text + "$]", baglanti);

                    // bağlantıyı açıyoruz.
                    baglanti.Open();

                    // Gelen verileri DataAdapter'e atıyoruz.
                    using (OleDbDataAdapter da = new OleDbDataAdapter(komut))
                    {
                        // Grid'imiz için bir DataTable oluşturuyoruz.
                        DataTable data = new DataTable();

                        // DataAdapter'da ki verileri data adındaki DataTable'a dolduruyoruz.
                        da.Fill(data);

                        // FAAturaList'i temizle, çünkü yeni verileri ekleyeceğiz
                        FAAturaList.Clear();

                        foreach (DataRow row in data.Rows)
                        {
                            Faturalar fatura = new Faturalar
                            {
                                TARİH = row["TARİH"].ToString(),
                                BELGE_TÜRÜ = row["BELGE TÜRÜ"].ToString(),
                                BELGE_NO = row["BELGE NO"].ToString()
                            };

                            FAAturaList.Add(fatura);
                        }

                        MessageBox.Show("Fiyat Listesi Yüklendi");
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata alırsak ekrana bastırıyoruz.
                MessageBox.Show(ex.Message);
            }

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
                    dataRow[cell.ColumnIndex] = cell.Value;
                }
                dt.Rows.Add(dataRow);
            }

            // Excel uygulamasını başlatın
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelApp.Visible = true;

            // Yeni bir Excel çalışma kitabı oluşturun
            Microsoft.Office.Interop.Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];

            // DataTable'ı Excel çalışma sayfasına aktarın (tablo başlıklarını da dahil etmek için)
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
                    worksheet.Cells[rowIndex, j + 1] = dt.Rows[i][j].ToString();
                }
            }
        }
    }
}
