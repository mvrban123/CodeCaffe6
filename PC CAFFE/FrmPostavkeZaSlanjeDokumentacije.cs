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
using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace PCPOS
{
    public partial class FrmPostavkeZaSlanjeDokumentacije : Form
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FrmPostavkeZaSlanjeDokumentacije()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event handlers
        /// </summary>
        private void buttonPrintanje_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nakon pritiska na gumb OK, svi označeni dokumenti generirat će se i bit poslani knjigovodstvu.", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MessageBox.Show("Molimo Vas da NE DIRATE NIŠTA do trenutka kad dobijete poruku da je mail poslan. Na slabijim računalima ovo može potrajati i par minuta.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            ObrisiDirektorijAkoPostoji();
            StvoriDirektorijAkoNePostoji();
            ProvjeriIzlazneListe(); // Fale izdatnice, otpis robe i usklada robe - Dejan: "Jos nije napravljeno."
            ProvjeriPromet();
            PosaljiEmail();

        }


        /// <summary>
        /// Ova metoda sluzi kako bi se obrisao direktorij Dokumenti ukoliko on postoji.
        /// Time pridobivamo da se stari dokumenti koji su se nalazili u tom folderu obrišu.
        /// Nakon toga slijedi stvaranje novog direktorija s novim dokumentima.
        /// </summary>
        private void ObrisiDirektorijAkoPostoji()
        {
            string Path = AppDomain.CurrentDomain.BaseDirectory + "Dokumenti";
            if (Directory.Exists(Path))
            {
                //Brise pdfove unutra
                DirectoryInfo di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Dokumenti"); 
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                
                //Brise direktorij
                Directory.Delete(Path);
            }
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
            System.Threading.Thread.Sleep(500);

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
            //Izdatnice
            if (checkBoxIzdatnice.Checked)
            {
                IzlazniRacuni frmIzlazniRacuni = new IzlazniRacuni(true, "izd", dateTimePickerPocetni.Value, dateTimePickerZavrsni.Value);
                frmIzlazniRacuni.ShowDialog();
            }
            //Otpis robe
            if (checkBoxOtpisRobe.Checked)
            {
                IzlazniRacuni frmIzlazniRacuni = new IzlazniRacuni(true, "otp_rob", dateTimePickerPocetni.Value, dateTimePickerZavrsni.Value);
                frmIzlazniRacuni.ShowDialog();
            }
            //Usklada robe ?
            if (checkBoxUskladaRobe.Checked)
            {
                //Dejan: "Jos nije napravljeno"
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
                IzlazniDokumenti.ObracunForm frmObracun = new IzlazniDokumenti.ObracunForm(true, dateTimePickerPocetni.Value, dateTimePickerZavrsni.Value);
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
                Kasa.frmPrometKase formPrometKase = new Kasa.frmPrometKase(true, dateTimePickerPocetni.Value, dateTimePickerZavrsni.Value);
                formPrometKase.ShowDialog();
            }
            //Promet po prodajnoj robi
            if (checkBoxPice.Checked)
            {
                Caffe.frmProdajnaRoba formProdajnaRoba = new Caffe.frmProdajnaRoba(true, "Pice", dateTimePickerPocetni.Value, dateTimePickerZavrsni.Value);
                formProdajnaRoba.ShowDialog();
            }

            if (checkBoxHrana.Checked)
            {
                Caffe.frmProdajnaRoba formProdajnaRoba = new Caffe.frmProdajnaRoba(true, "Hrana", dateTimePickerPocetni.Value, dateTimePickerZavrsni.Value);
                formProdajnaRoba.ShowDialog();
            }

            if (checkBoxTrgovackaRoba.Checked)
            {
                Caffe.frmProdajnaRoba formProdajnaRoba = new Caffe.frmProdajnaRoba(true, "TrgRoba", dateTimePickerPocetni.Value, dateTimePickerZavrsni.Value);
                formProdajnaRoba.ShowDialog();
            }

            if (checkBoxUkupno.Checked)
            {
                Caffe.frmProdajnaRoba formProdajnaRoba = new Caffe.frmProdajnaRoba(true, "Ukupno", dateTimePickerPocetni.Value, dateTimePickerZavrsni.Value);
                formProdajnaRoba.ShowDialog();
            }
        }

        /// <summary>
        /// Ova metoda koristi se za slanje e-maila.
        /// </summary>
        private void PosaljiEmail()
        {
            DataTable DTpodaci = classSQL.select_settings("SELECT * FROM podaci_tvrtka WHERE id='1'", "podaci_tvrtka").Tables[0];
            string imetvrtke = DTpodaci.Rows[0]["ime_tvrtke"].ToString();
            string email = DTpodaci.Rows[0]["email_knjigovodstvo"].ToString();
            if (!EmailOdRacunovodstvaUnesen(email))
                return;

            try
            {
                //Smtp
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com"); // SmtpServerName = smtp.gmail.com
                SmtpServer.Port = 587;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.Credentials = new System.Net.NetworkCredential("dokumenti.codeit@gmail.com", "Dejan102");
                //SmtpServer.Credentials = new System.Net.NetworkCredential("prometi@code-it.hr", "Prometi123");
                SmtpServer.EnableSsl = true;

                //E-Mail
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("dokumenti.codeit@gmail.com");
                //mail.From = new MailAddress("prometi@code-it.hr");
                mail.To.Add(email);
                mail.Subject = "Dokumenti za knjigovodstvo - " + imetvrtke;
                mail.Body = $@"Poštovani! U prilogu se nalaze dokumenti potrebni za računovodstvo od datuma {dateTimePickerPocetni.Value.ToString("dd/MM/yyyy")} do datuma {dateTimePickerZavrsni.Value.ToString("dd/MM/yyyy")}.";
                StaviPDFoveUEmail(mail);

                //Send E-Mail
                SmtpServer.Send(mail);
                MessageBox.Show("Odabrani dokumenti poslani su knjigovodstvu.", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška u slanju E-Maila knjigovodstvu." + ex.ToString());
            }
        }

        /// <summary>
        /// Ova metoda provjerava ukoliko je email knjigovodstva koji je unesen u Postavke -> Postavke o tvrtki ispravan.
        /// </summary>
        private bool EmailOdRacunovodstvaUnesen(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Neispravan E-Mail računovodstva. Idite u Postavke -> Podaci o tvrtki te upišite ispravan E-Mail knjigovodstva.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Ova metoda stavlja PDFove koji se nalaze u folderu Dokumenti u Mail prije slanja istog u računovodstvo.
        /// </summary>
        private void StaviPDFoveUEmail(MailMessage mail)
        {
            DirectoryInfo d = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Dokumenti");
            FileInfo[] Files = d.GetFiles("*.pdf");
            foreach (FileInfo file in Files)
            {
                mail.Attachments.Add(new Attachment(AppDomain.CurrentDomain.BaseDirectory + $@"Dokumenti\{file.Name}"));
            }
        }
    }
}
