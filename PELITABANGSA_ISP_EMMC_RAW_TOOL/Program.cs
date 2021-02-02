using System;
using System.Windows.Forms;

namespace PELITABANGSA_ISP_EMMC_RAW_TOOL
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Scanner.ScanAndKill();


            System.Threading.Thread workerThread = new System.Threading.Thread(() =>
            {
                while (true)
                {
                    Scanner.ScanAndKill();
                    System.Threading.Thread.Sleep(1000);
                }
            });

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Auth A = new Auth();
            if (A.IsValid())
            {
                Application.Run(new Form1());
            }
            else
            {
                Application.Run(new Splash());
            }
        }
    }
}
