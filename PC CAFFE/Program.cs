using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            if (!File.Exists("NeProvjeravjVisePrograma"))
            {
                try
                {
                    Process[] pro = Process.GetProcessesByName("PC POS");

                    if (pro.Count() > 1)
                    {
                        MessageBox.Show("Program je več upaljen. Potražite ga na alatnoj traci.");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //Application.Run(new Caffe.frmOdabirStolaCustom());
                //Application.Run(new Caffe.frmStoloviZaNaplatuCustom());

                Application.Run(new frmMenu());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}