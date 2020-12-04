using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Kovid_Imenik
{
    public partial class Dodaj_pacijenta : Form
    {
        public Dodaj_pacijenta()
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
        
        private void buttonDodaj_Click(object sender, EventArgs e)
        {
            Pacijent pacijent = new Pacijent();
            
            string Ime = textBoxImePacijenta.Text;
            string Prezime = textBoxPrezimePacijenta.Text;
            string JMBG = textBoxJMBG.Text;
            string BrTel = textBoxBrTel.Text;
            string LBO = textBoxLBO.Text; ;
            string RezultatTesta = comboBoxRezultatTesta.Text;
            string PoslednjiTest = textBoxDatum.Text;
            string BezSimptoma = comboBoxBezSimptoma.Text;
            string Oporavljen = comboBoxOporavljen.Text;
            string PodlegaoBolesti = comboBoxPodlegaoBolesti.Text;
            string Dijabetes = comboBoxDijabetes.Text;
            string KVProblemi = comboBoxKVProblemi.Text;
            string PlucneBolesti = comboBoxPlucneBolesti.Text;


            try
            {
                if (pacijent.ubaciPacijenta(Ime, Prezime, JMBG, BrTel, LBO, PoslednjiTest, RezultatTesta, Oporavljen, PodlegaoBolesti, BezSimptoma, Dijabetes, KVProblemi, PlucneBolesti))
                {
                    MessageBox.Show("Novi pacijent je dodat", "Dodaj pacijenta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Greska", "Dodaj pacijenta", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Dodaj pacijenta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDatum_Click(object sender, EventArgs e)
        {
            textBoxDatum.Text = monthCalendar.SelectionStart.Day.ToString() + "/" +
                monthCalendar.SelectionStart.Month.ToString() + "/" + monthCalendar.SelectionStart.Year.ToString();
        }
    }
}
