using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class IzlazniRacuni : Form
    {
        private bool isFaktura = true;

        public IzlazniRacuni()
        {
            InitializeComponent();
        }

        private void IzlazniRacuni_Load(object sender, EventArgs e)
        {
            OnFormLoad();
        }

        /// <summary>
        /// Method used to execute actions on Form Load
        /// </summary>
        private void OnFormLoad()
        {
            cbDatum.Checked = true;
            ManageDatumGroupBoxContent();
            FillComboBoxDokument();
        }

        /// <summary>
        /// Method used to manage "Brojevi" GroupBox content
        /// </summary>
        private void ManageBrojeviGroupBoxContent()
        {
            if (cbBrojevi.Checked)
                cbDatum.Checked = false;

            // Brojevi
            lblOdRacuna.Enabled = cbBrojevi.Checked;
            lblDoRacuna.Enabled = cbBrojevi.Checked;
            lblKalkulacija.Enabled = cbBrojevi.Checked;

            // Datum
            lblOdDatuma.Enabled = !cbBrojevi.Checked;
            lblDoDatuma.Enabled = !cbBrojevi.Checked;
            dtpOdDatuma.Enabled = !cbBrojevi.Checked;
            dtpDoDatuma.Enabled = !cbBrojevi.Checked;
        }

        /// <summary>
        /// Method used to manage "Datum" GroupBox content
        /// </summary>
        private void ManageDatumGroupBoxContent()
        {
            if (cbDatum.Checked)
                cbBrojevi.Checked = false;

            // Brojevi
            lblOdRacuna.Enabled = cbBrojevi.Checked;
            lblDoRacuna.Enabled = cbBrojevi.Checked;
            lblKalkulacija.Enabled = cbBrojevi.Checked;
            tbOdRacuna.Enabled = cbBrojevi.Checked;
            tbDoRacuna.Enabled = cbBrojevi.Checked;

            // Datum
            lblOdDatuma.Enabled = !cbBrojevi.Checked;
            lblDoDatuma.Enabled = !cbBrojevi.Checked;
        }

        /// <summary>
        /// Method used to fill ComboBox "cmbDokument"
        /// </summary>
        private void FillComboBoxDokument()
        {
            DataTable DTSK = new DataTable("IzlazniRacuni");

            DTSK.Columns.Add("id", typeof(string));
            DTSK.Columns.Add("naziv", typeof(string));

            DTSK.Rows.Add("kalk", "Kalkulacije");
            DTSK.Rows.Add("prim", "Primke");
            DTSK.Rows.Add("fakt", "Fakture");
            DTSK.Rows.Add("izd", "Izdatnica");
            DTSK.Rows.Add("otp_rob", "Otpis robe");

            cmbDokument.DataSource = DTSK;
            cmbDokument.DisplayMember = "naziv";
            cmbDokument.ValueMember = "id";

            GetSkladiste();
        }

        /// <summary>
        /// Method used to fill ComboBox "cmbSkladiste"
        /// </summary>
        private void GetSkladiste()
        {
            try
            {
                if ((cmbDokument.SelectedValue.ToString() != "fakt" || Util.Korisno.oibTvrtke != Class.Postavke.OIB_PC1))
                {
                    if(isFaktura)
                    {
                        DataTable DTSK1 = new DataTable("IzlazniRacuniSkladiste");

                        string sql = "SELECT * FROM skladiste";
                        DTSK1 = classSQL.select(sql, "skl").Tables[0];

                        cmbSkladiste.DataSource = DTSK1;
                        cmbSkladiste.DisplayMember = "skladiste";
                        cmbSkladiste.ValueMember = "id_skladiste";
                        isFaktura = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbBrojevi_CheckedChanged(object sender, EventArgs e)
        {
            ManageBrojeviGroupBoxContent();
        }

        private void cbDatum_CheckedChanged(object sender, EventArgs e)
        {
            ManageDatumGroupBoxContent();
        }

        private void btnIspis_Click(object sender, EventArgs e)
        {
            if(cbDatum.Checked)
            {
                if(cmbDokument.SelectedValue.ToString() == "fakt")
                {
                    Report.Kalkposkl.FaktureForm faktureForm = new Report.Kalkposkl.FaktureForm();
                    faktureForm.datumOD = dtpOdDatuma.Value.Date;
                    faktureForm.datumDO = dtpDoDatuma.Value.Date;
                    faktureForm.ShowDialog();
                }

                if (cmbDokument.SelectedValue.ToString() == "kalk")
                {
                    Report.Kalkposkl.KalkulacijeForm kalkulacijeForm = new Report.Kalkposkl.KalkulacijeForm();
                    kalkulacijeForm.datumOD = dtpOdDatuma.Value.Date;
                    kalkulacijeForm.datumDO = dtpDoDatuma.Value.Date;
                    kalkulacijeForm.ShowDialog();
                }

                if (cmbDokument.SelectedValue.ToString() == "prim")
                {
                    Report.Kalkposkl.PrimkeForm primkeForm = new Report.Kalkposkl.PrimkeForm();
                    primkeForm.BrojFakDO = tbDoRacuna.Text;
                    primkeForm.documenat = cmbDokument.SelectedValue.ToString();
                    primkeForm.prema_rac = cbBrojevi.Checked;
                    primkeForm.bool1 = cbSkladiste.Checked;
                    primkeForm.skladiste_odabir = cmbSkladiste.SelectedValue.ToString();
                    primkeForm.skladiste = cmbSkladiste.Text;
                    primkeForm.BrojFakOD = tbOdRacuna.Text;
                    primkeForm.datumOD = dtpOdDatuma.Value.Date;
                    primkeForm.datumDO = dtpDoDatuma.Value.Date;
                    primkeForm.ShowDialog();
                }
            }
        }
    }
}
