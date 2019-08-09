using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        private void buttonPrintanje_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nakon pritiska na gumb OK, svi označeni dokumenti generirat će se i bit poslani knjigovodstvu.", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MessageBox.Show("Molimo Vas da NE DIRATE NIŠTA do trenutka kad dobijete poruku da je mail poslan. Na slabijim računalima ovo može potrajati i par minuta.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            StvoriDirektorijAkoNePostoji();
            ProvjeriIzlazneListe(); // Fale izdatnice, otpis robe i usklada robe
            ProvjeriPromet();
            //ŠaljiMail();
            

            MessageBox.Show("Odabrani dokumenti poslani su knjigovodstvu.", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Ova metoda služi za dobivanje emaila knjigovodstva iz kompaktne baze podataka
        /// </summary>
        /// <returns></returns>
        private string PreuzmiMailKnjigovodstvaIzKompaktneBaze()
        {
            DataTable DTpodaci = classSQL.select_settings("SELECT * FROM podaci_tvrtka WHERE id='1'", "podaci_tvrtka").Tables[0];
            return DTpodaci.Rows[0]["email_knjigovodstvo"].ToString();
        }

        /// <summary>
        /// Ova metoda stvara direktorij Dokumenti (ako ne postoji) u koji se spremaju određeni PDFovi
        /// </summary>
        private void StvoriDirektorijAkoNePostoji()
        {
            string Path = AppDomain.CurrentDomain.BaseDirectory + "Dokumenti";
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);
        }

        /// <summary>
        /// Ova metoda služi za generiranje pdfova onih izlaznih listi koje su označene
        /// </summary>
        private void ProvjeriIzlazneListe()
        {
            //Kalkulacije
            if (checkBoxKalkulacije.Checked)
            {
                IzlazniRacuni frmIzlazniRacuni = new IzlazniRacuni(true, "kalk", dateTimePickerPocetni.Value, dateTimePickerZavrsni.Value);
                frmIzlazniRacuni.ShowDialog();
            }
            //Primke
            if (checkBoxPrimke.Checked)
            {
                IzlazniRacuni frmIzlazniRacuni = new IzlazniRacuni(true, "prim", dateTimePickerPocetni.Value, dateTimePickerZavrsni.Value);
                frmIzlazniRacuni.ShowDialog();
            }
            //Fakture
            if (checkBoxPrimke.Checked)
            {
                IzlazniRacuni frmIzlazniRacuni = new IzlazniRacuni(true, "fakt", dateTimePickerPocetni.Value, dateTimePickerZavrsni.Value);
                frmIzlazniRacuni.ShowDialog();
            }
            //Izdatnice ? - Samo se 1 moze printati odjednom? Kaj s tim?
            if (checkBoxIzdatnice.Checked)
            {
                /*PCPOS.Robno.frmSveIzdatnice frmAllIzdatnice = new PCPOS.Robno.frmSveIzdatnice();
                frmAllIzdatnice.ShowDialog();*/
            }
            //Otpis robe ?
            if (checkBoxOtpisRobe.Checked)
            {

            }
            //Usklada robe ?
            if(checkBoxUskladaRobe.Checked)
            {

            }
        }

        private void ProvjeriPromet()
        {
            //PP-MI-PO Obrazac
            if (checkBoxPPMIPOObrazac.Checked)
            {
                IzlazniDokumenti.PPMIPOForm ppmipoForm = new IzlazniDokumenti.PPMIPOForm(true, dateTimePickerPocetni.Value, dateTimePickerZavrsni.Value);
                ppmipoForm.ShowDialog();
            }
            //Obracun poreza i prometa
            if (checkBoxObracunPorezaIPrometa.Checked)
            {
                IzlazniDokumenti.ObracunForm frmObracun = new IzlazniDokumenti.ObracunForm(true,dateTimePickerPocetni.Value,dateTimePickerZavrsni.Value);
                frmObracun.ShowDialog();
            }
            //Obracun grupe proizvoda
            if (checkBoxObracunGrupeProizvoda.Checked)
            {
                IzlazniDokumenti.ObracunGrupeProizvodaForm obracunGrupeProizvodaForm = new IzlazniDokumenti.ObracunGrupeProizvodaForm(true, dateTimePickerPocetni.Value, dateTimePickerZavrsni.Value);
                obracunGrupeProizvodaForm.ShowDialog();
            }
            //Obracun prometa po danima
            if (checkBoxObracunPrometaPoDanima.Checked)
            {
                Report.PrometiPoDanima.frmIspisProdajnihArtiklaPoDanima formIspisProdajnihArtiklaPoDanima = new Report.PrometiPoDanima.frmIspisProdajnihArtiklaPoDanima(true, dateTimePickerPocetni.Value, dateTimePickerZavrsni.Value);
                formIspisProdajnihArtiklaPoDanima.ShowDialog();
            }
            //Promet kase
            if (checkBoxPrometKase.Checked)
            {
                PCPOS.Kasa.frmPrometKase formPrometKase = new PCPOS.Kasa.frmPrometKase(true, dateTimePickerPocetni.Value, dateTimePickerZavrsni.Value);
                formPrometKase.ShowDialog();
            }
            //Promet po prodajnoj robi
            if (checkBoxPice.Checked)
            {
                PCPOS.Caffe.frmProdajnaRoba formProdajnaRoba = new PCPOS.Caffe.frmProdajnaRoba(true, "Pice", dateTimePickerPocetni.Value, dateTimePickerZavrsni.Value);
                formProdajnaRoba.ShowDialog();
            }

            if (checkBoxHrana.Checked)
            {
                PCPOS.Caffe.frmProdajnaRoba formProdajnaRoba = new PCPOS.Caffe.frmProdajnaRoba(true, "Hrana", dateTimePickerPocetni.Value, dateTimePickerZavrsni.Value);
                formProdajnaRoba.ShowDialog();
            }

            if (checkBoxTrgovackaRoba.Checked)
            {
                PCPOS.Caffe.frmProdajnaRoba formProdajnaRoba = new PCPOS.Caffe.frmProdajnaRoba(true, "TrgRoba", dateTimePickerPocetni.Value, dateTimePickerZavrsni.Value);
                formProdajnaRoba.ShowDialog();
            }

            if (checkBoxUkupno.Checked)
            {
                PCPOS.Caffe.frmProdajnaRoba formProdajnaRoba = new PCPOS.Caffe.frmProdajnaRoba(true, "Ukupno",dateTimePickerPocetni.Value, dateTimePickerZavrsni.Value);
                formProdajnaRoba.ShowDialog();
            }
        }


        /// <summary>
        /// EventHandlers
        /// </summary>
      

        private void checkBoxPice_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPice.Checked)
            {
                checkBoxHrana.Checked = false;
                checkBoxTrgovackaRoba.Checked = false;
                checkBoxUkupno.Checked = false;
            }
        }

        private void checkBoxHrana_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxHrana.Checked)
            {
                checkBoxPice.Checked = false;
                checkBoxTrgovackaRoba.Checked = false;
                checkBoxUkupno.Checked = false;
            }
        }

        private void checkBoxTrgovackaRoba_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTrgovackaRoba.Checked)
            {
                checkBoxPice.Checked = false;
                checkBoxHrana.Checked = false;
                checkBoxUkupno.Checked = false;
            }
        }

        private void checkBoxUkupno_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxUkupno.Checked)
            {
                checkBoxPice.Checked = false;
                checkBoxHrana.Checked = false;
                checkBoxTrgovackaRoba.Checked = false;
            }
        }
    }
}
