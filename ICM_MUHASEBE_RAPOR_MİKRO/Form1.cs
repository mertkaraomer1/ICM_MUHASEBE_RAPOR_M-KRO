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

            SetCircularRegion(button1);
            SetCircularRegion(button2);
            SetCircularRegion(button3);
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
            FİŞ_EŞLEŞTİRME FFE = new FİŞ_EŞLEŞTİRME();
            FFE.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Ana form kapatıldığında tüm uygulamayı kapat
                Application.Exit();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FİNANS_RAPORU FR=new FİNANS_RAPORU();
            FR.Show();
        }
    }
}
