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
    }
}
