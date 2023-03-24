
using System;
using System.Threading;
using System.Windows.Forms;

namespace Project
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            bool instanceCountOne = false;
            using (Mutex mtex = new Mutex(true, "MASUInstaller", out instanceCountOne))
            {
                if (!instanceCountOne)
                {
                    return;
                }

                try
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new WebInstallerForm());
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Алдаа!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
    }
}
