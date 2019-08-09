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
    public partial class SveUskladaRobeNaSkladistuForm : Form
    {
        public SveUskladaRobeNaSkladistuForm()
        {
            InitializeComponent();
            dohvatiPodatkeOUskladamaRobe();
        }
        private void dohvatiPodatkeOUskladamaRobe()
        {
            string sql = "select ur.id_usklade,ur.datum,ur.napomena,zaposlenici.ime,zaposlenici.prezime from usklada_robe ur join zaposlenici on zaposlenici.id_zaposlenik=ur.izradio";
            DataSet DSUskladeRobe = classSQL.select(sql, "usklada_robe");
            sveUskladeView.DataSource = DSUskladeRobe.Tables[0];
            sveUskladeView.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
            Report.Uskladaskladista.UskladaSkladistaReportForm aa = new Report.Uskladaskladista.UskladaSkladistaReportForm();
            aa.broj_dokumenta = int.Parse(sveUskladeView.CurrentRow.Cells[0].FormattedValue.ToString());
            //aa.skladiste = int.Parse(sveUskladeView.CurrentRow.Cells["id_skladiste"].FormattedValue.ToString());
            aa.ShowDialog();
=======
            MessageBox.Show("BLAAAAAAA");
>>>>>>> b9de224eb5271badb0321ba9b993b2dd078ef15b
        }
    }
}
