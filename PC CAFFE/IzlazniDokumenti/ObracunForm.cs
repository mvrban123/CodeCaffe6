using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCPOS.IzlazniDokumenti
{
    public partial class ObracunForm : Form
    {
        public ObracunForm()
        {
            InitializeComponent();
        }

        private void btnIspisi_Click(object sender, EventArgs e)
        {
            if (dtpOd.Value < dtpDo.Value)
            {
                Report.IzlazniDokumenti.ObracunReport form = new Report.IzlazniDokumenti.ObracunReport();
                form.datumOd = dtpOd.Value;
                form.datumDo = dtpDo.Value;
                form.ShowDialog();
            }
            else
                MessageBox.Show("\"Datum od\" ne može biti veći od \"Datum do\".");
        }
    }
}
