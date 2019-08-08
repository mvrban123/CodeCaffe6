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
    public partial class FrmPostavkeZaSlanjeDokumentacije : Form
    {
        /// <summary>
        /// Global variables
        /// </summary>
        private string SendingEmail; // mail firme s koje se saljeju dokumenti prema knjigovodstvu
        private string ReceiverEmail; // mail knjigovodstva

        /// <summary>
        /// Constructor
        /// </summary>
        public FrmPostavkeZaSlanjeDokumentacije()
        {
            InitializeComponent();

            SendingEmail = "malcom.houston98@gmail.com";
            ReceiverEmail = PreuzmiMailKnjigovodstvaIzKompaktneBaze();
        }

        /// <summary>
        /// Event handlers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPrintanje_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nakon pritiska na gumb OK, svi označeni dokumenti generirat će se i bit poslani knjigovodstvu.", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MessageBox.Show("Molimo Vas da NE DIRATE NIŠTA do trenutka kad dobijete poruku da je mail poslan.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //Dokumenti
            if (checkBoxKalkulacije.Checked)
            {
                IzlazniRacuni frmIzlazniRacuni = new IzlazniRacuni(true,"kalk",dateTimePickerPocetni.Value,dateTimePickerZavrsni.Value);
                frmIzlazniRacuni.ShowDialog();
            }

            if (checkBoxPrimke.Checked)
            {
                IzlazniRacuni frmIzlazniRacuni = new IzlazniRacuni(true, "prim", dateTimePickerPocetni.Value, dateTimePickerZavrsni.Value);
                frmIzlazniRacuni.ShowDialog();
            }
            

            MessageBox.Show("Odabrani dokumenti poslani su knjigovodstvu.", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Methods
        /// </summary>
        /// <returns></returns>
        private string PreuzmiMailKnjigovodstvaIzKompaktneBaze()
        {
            DataTable DTpodaci = classSQL.select_settings("SELECT * FROM podaci_tvrtka WHERE id='1'", "podaci_tvrtka").Tables[0];
            return DTpodaci.Rows[0]["email_knjigovodstvo"].ToString();
        }
    }
}
