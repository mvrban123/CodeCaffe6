using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Caffe
{
    public partial class frmProdajnaRoba : Form
    {
        public frmProdajnaRoba()
        {
            InitializeComponent();
        }

        private void frmProdajnaRoba_Load(object sender, EventArgs e)
        {
            SetCB();
        }

        private DataTable DT_Zaposlenik;
        private DataTable DT_Skladiste;
        private DataTable DT_Ducan;
        private DataTable DT_Zaposlenik2 = new DataTable();
        private DataTable DT_Skladiste2 = new DataTable();
        private DataTable DT_Ducan2 = new DataTable();
        //DataTable DT_Ducan;

        private void SetCB()
        {
            //fill komercijalist
            DT_Ducan = classSQL.select("SELECT ime_ducana,id_ducan FROM ducan", "ducan").Tables[0];
            cbDucan.DataSource = DT_Ducan;
            cbDucan.DisplayMember = "ime_ducana";
            cbDucan.ValueMember = "id_ducan";

            //fill grupe
            DataTable DT_cbGrupa = classSQL.select("SELECT grupa,id_grupa FROM grupa", "grupa").Tables[0];
            cbGrupa.DataSource = DT_cbGrupa;
            cbGrupa.DisplayMember = "grupa";
            cbGrupa.ValueMember = "id_grupa";

            //fill komercijalist
            DT_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici").Tables[0];
            cbZaposlenik.DataSource = DT_Zaposlenik;
            cbZaposlenik.DisplayMember = "IME";
            cbZaposlenik.ValueMember = "id_zaposlenik";

            DT_Skladiste2.Columns.Add("id_podgrupa", typeof(string));
            DT_Skladiste2.Columns.Add("naziv", typeof(string));

            DT_Skladiste = classSQL.select("SELECT * FROM podgrupa", "podgrupa").Tables[0];
            for (int i = 0; i < DT_Skladiste.Rows.Count; i++)
            {
                DT_Skladiste2.Rows.Add(DT_Skladiste.Rows[i]["id_podgrupa"].ToString(), DT_Skladiste.Rows[i]["naziv"].ToString());
            }

            cbPodgrupa.DataSource = DT_Skladiste2;
            cbPodgrupa.DisplayMember = "naziv";
            cbPodgrupa.ValueMember = "id_podgrupa";
        }

        private void pic_MouseEnter(object sender, EventArgs e)
        {
            Button PB = ((Button)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w + 7, h + 7);
        }

        private void pic_MouseLeave(object sender, EventArgs e)
        {
            Button PB = ((Button)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w - 7, h - 7);
        }

        private void dtpOD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.F1)
            {
                btnIspis.PerformClick();
            }
        }

        private void btnIspis_Click(object sender, EventArgs e)
        {
            Report.Liste.frmListe aa = new Report.Liste.frmListe();
            aa.datumOD = dtpOD.Value;
            aa.datumDO = dtpDO.Value;
            aa.ImeForme = "Promet po robi";
            aa.dokumenat = "PrometRobe";
            if (txtArtikl.Text != "")
            {
                aa.artikl = txtArtikl.Text;
            }
            if (chbDucan.Checked)
            {
                aa.ducan = cbDucan.SelectedValue.ToString();
            }
            if (chbBlagajnik.Checked)
            {
                aa.blagajnik = cbZaposlenik.SelectedValue.ToString();
            }
            if (chbPodgrupa.Checked)
            {
                aa.id_podgrupa = cbPodgrupa.SelectedValue.ToString();
            }
            if (chbGrupa.Checked)
            {
                aa.grupa = cbGrupa.SelectedValue.ToString();
            }

            aa.ShowDialog();
        }
    }
}