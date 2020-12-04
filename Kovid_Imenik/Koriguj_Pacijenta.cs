using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kovid_Imenik
{
    public partial class Koriguj_Pacijenta : Form
    {
        public Koriguj_Pacijenta()
        {
            InitializeComponent();
        }

        private void buttonZatvori_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonMinimiziraj_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void buttonOdaberiPac_Click(object sender, EventArgs e)
        {
            //prikazi formu za izbor pacijenta koga zelimo
            Izaberi_Pacijenta izaberiPac = new Izaberi_Pacijenta();
            izaberiPac.ShowDialog(this);

            try
            {
                //pacijentov ID
                int pacijentovID = Convert.ToInt32(textBoxPacID.Text = izaberiPac.dataGridView1.CurrentRow.Cells[0].Value.ToString());

                Pacijent pacijent = new Pacijent();
                DataTable table = pacijent.nadjiPacPomocuID(pacijentovID);

                textBoxPacID.Text = table.Rows[0]["ID"].ToString();
                textBoxImePacijenta.Text = table.Rows[0]["Ime"].ToString();
                textBoxPrezimePacijenta.Text = table.Rows[0]["Prezime"].ToString();
                textBoxJMBG.Text = table.Rows[0]["JMBG"].ToString();
                textBoxLBO.Text = table.Rows[0]["LBO"].ToString();
                textBoxBrTel.Text = table.Rows[0]["BrTel"].ToString();
                textBoxDatum.Text = table.Rows[0]["PoslednjiTest"].ToString();
                textBoxRezultatTesta.Text = table.Rows[0]["RezultatTesta"].ToString();
                textBoxOporavljen.Text = table.Rows[0]["Oporavljen"].ToString();
                textBoxPodlegaoBolesti.Text = table.Rows[0]["PodlegaoBolesti"].ToString();
                textBoxBezSimptoma.Text = table.Rows[0]["BezSimptoma"].ToString();
                textBoxDijabetes.Text = table.Rows[0]["Dijabetes"].ToString();
                textBoxKVProblemi.Text = table.Rows[0]["KVProblemi"].ToString();
                textBoxPlucneBolesti.Text = table.Rows[0]["PlucneBolesti"].ToString();
            }
            catch
            {

            }

        }

        //dugme koriguj
        private void buttonKoriguj_Click(object sender, EventArgs e)
        {
            Pacijent pacijent = new Pacijent();

            string Ime = textBoxImePacijenta.Text;
            string Prezime = textBoxPrezimePacijenta.Text;
            string JMBG = textBoxJMBG.Text;
            string BrTel = textBoxBrTel.Text;
            string LBO = textBoxLBO.Text; ;
            string RezultatTesta = textBoxRezultatTesta.Text;
            string PoslednjiTest = textBoxDatum.Text;
            string BezSimptoma = textBoxBezSimptoma.Text;
            string Oporavljen = textBoxOporavljen.Text;
            string PodlegaoBolesti = textBoxPodlegaoBolesti.Text;
            string Dijabetes = textBoxDijabetes.Text;
            string KVProblemi = textBoxKVProblemi.Text;
            string PlucneBolesti = textBoxPlucneBolesti.Text;


            try
            {
                int pacijentovID = Convert.ToInt32(textBoxPacID.Text);
                if (pacijent.korigujPacijenta(pacijentovID, Ime, Prezime, JMBG, BrTel, LBO, PoslednjiTest, RezultatTesta, Oporavljen, PodlegaoBolesti, BezSimptoma, Dijabetes, KVProblemi, PlucneBolesti))
                {
                    MessageBox.Show("Podaci pacijenta su azurirani", "Koriguj podatke pacijenta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Greska", "Koriguj podatke pacijenta", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Koriguj podatke pacijenta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      
        private void buttonDatum_Click(object sender, EventArgs e)
        {
            textBoxDatum.Text = monthCalendar.SelectionStart.Day.ToString() + "/" +
                monthCalendar.SelectionStart.Month.ToString() + "/" + monthCalendar.SelectionStart.Year.ToString();
        }

    }
    
}
