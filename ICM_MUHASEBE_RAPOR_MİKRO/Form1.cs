using ICM_MUHASEBE_RAPOR_MİKRO.Context;
using System.Data;
using System.Drawing.Drawing2D;

namespace ICM_MUHASEBE_RAPOR_MİKRO
{
    public partial class Form1 : Form
    {
        private MyDbContext dbContext;
        public Form1()
        {
            dbContext = new MyDbContext();
            InitializeComponent();
        }
        DataTable table = new DataTable();
        private void Form1_Load(object sender, EventArgs e)
        {
            //table.Clear();
            //table.Columns.Clear();
            //table.Rows.Clear();
            //table.Columns.Add("SATIR NO");
            //table.Columns.Add("PROJE KODU");
            //table.Columns.Add("SERİ NO");
            //table.Columns.Add("SIRA NO");
            //table.Columns.Add("AÇIKLAMA");
            //table.Columns.Add("ONAYLAYAN KULLANICI NO");



            //var projectCodes = dbContext.SIPARISLER
            //    .Where(isEmir =>
            //    !string.IsNullOrEmpty(isEmir.sip_evrakno_seri)
            //    && (isEmir.sip_projekodu.EndsWith("/005") || isEmir.sip_projekodu.EndsWith("/006"))
            //    && isEmir.sip_tip == 0)
            //    .Select(isEmir => new
            //    {

            //        ProjectCode = isEmir.sip_projekodu,
            //        OrderSeries = isEmir.sip_evrakno_seri,
            //        OrderSira = isEmir.sip_evrakno_sira,
            //        SeriSira = isEmir.sip_evrakno_seri + '-' + isEmir.sip_evrakno_sira,
            //        OnaylayanKulNo = Convert.ToInt32(isEmir.sip_OnaylayanKulNo)
            //    })
            //   .Distinct()
            //    .ToList();
            //int sayac = 0;
            //foreach (var item in projectCodes)
            //{
            //    table.Rows.Add(sayac++, item.ProjectCode, item.OrderSeries, item.OrderSira, item.SeriSira, item.OnaylayanKulNo);

            //}
            //advancedDataGridView1.DataSource = table;

            SetCircularRegion(button1);
            SetCircularRegion(button2);
        }


        private void SetCircularRegion(Button button)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, button.Width, button.Height);
            button.Region = new Region(path);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            IHRACAT_RAPOR Ihr = new IHRACAT_RAPOR();
            Ihr.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FİŞ_EŞLEŞTİRME FFE=new FİŞ_EŞLEŞTİRME();
            FFE.Show();
        }
    }
}
