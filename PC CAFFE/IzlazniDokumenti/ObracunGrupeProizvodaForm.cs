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
    public partial class ObracunGrupeProizvodaForm : Form
    {
        public ObracunGrupeProizvodaForm()
        {
            InitializeComponent();
        }

        private void ObracunGrupeProizvodaForm_Load(object sender, EventArgs e)
        {
            LoadGrupe();
        }

        /// <summary>
        /// Method used to load "grupe" data and fill dropdown
        /// </summary>
        private void LoadGrupe()
        {
            DataTable DTgrupa = classSQL.select("SELECT id_grupa, grupa FROM grupa ORDER BY grupa ASC;", "grupa").Tables[0];
            cbGrupe.DisplayMember = "grupa";
            cbGrupe.ValueMember = "id_grupa";
            cbGrupe.DataSource = DTgrupa;
        }

        private void btnIspisi_Click(object sender, EventArgs e)
        {
            Report.GrupeProizvoda.ReportObracunGrupe form = new Report.GrupeProizvoda.ReportObracunGrupe();
            form.datumOd = dtpOd.Value;
            form.datumDo = dtpDo.Value;
            if (checkGrupa.Checked)
                form.grupa = "AND roba.id_grupa = " + cbGrupe.SelectedValue.ToString();
            form.ShowDialog(); 

        }
    }
}
