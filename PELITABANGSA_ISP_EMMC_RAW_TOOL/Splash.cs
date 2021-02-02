using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Diagnostics;

namespace PELITABANGSA_ISP_EMMC_RAW_TOOL
{
    public partial class Splash : MetroForm
    {
        private Form1 satu;
        private Auth auth = new Auth();
        public Splash()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (auth.IsValid(KEY.Text))
            {
                Go_On();
            }
            else
            {
                MetroMessageBox.Show(this, "Key Error");
            }
        }
        
        private void Splash_Load(object sender, EventArgs e)
        {
            ID.Text = auth.ID();
        }

        private void Go_On()
        {
            Hide();
            satu = new Form1();
            satu.Show();
        }

        private void metroLink1_Click(object sender, EventArgs e)
        {
            Process.Start("https://fb.me/mrivai89");
        }
    }
}
