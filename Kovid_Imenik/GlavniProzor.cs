using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Kovid_Imenik
{
    public partial class GlavniProzor : Form
    {
        public GlavniProzor()
        {
            InitializeComponent();
        }

        Moja_Baza_Podataka mojaBP = new Moja_Baza_Podataka();

       

        private void buttonZatvori_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonMinimiziraj_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        //kreiramo funkciju da prikaze korisnikovo korisnicko ime
        public void prikaziKorIme()
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            DataTable table = new DataTable();

            MySqlCommand command = new MySqlCommand("SELECT * FROM tabela.korisnici WHERE ID=@ID",mojaBP.getConnection);

            command.Parameters.Add("@ID", MySqlDbType.Int32).Value = Uopstenje.UopsteniKorisnickiID;

            adapter.SelectCommand = command;

            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                //prikazi korisnicko ima
                labelKorIme.Text = "Dobrodosli nazad ( " + table.Rows[0]["KorIme"] + ")";
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void GlavniProzor_Load(object sender, EventArgs e)
        {
            prikaziKorIme();
        }

        private void labelKorigujKorisnikovePodatke_Click(object sender, EventArgs e)
        {
            Koriguj_svoje_podatke korigujKorisnika = new Koriguj_svoje_podatke();
            korigujKorisnika.Show(this);
        }
        //osvezi korisnikove podatke
        private void labelOsveziPod_Click(object sender, EventArgs e)
        {
            prikaziKorIme();
        }
        
        //dodaj pacijenta
        private void buttonDodajPac_Click(object sender, EventArgs e)
        {
            //prikazi dodaj formu
            Dodaj_pacijenta dodajPac = new Dodaj_pacijenta();
            dodajPac.Show(this);
        }

        private void buttonKorigujPac_Click(object sender, EventArgs e)
        {
            //prikazi izaberi formu
            Koriguj_Pacijenta korigujPac = new Koriguj_Pacijenta();
            korigujPac.Show(this);
        }
        //dugme za brisanje pacijenta iz baze podataka
        private void buttonIzbrisiPac_Click(object sender, EventArgs e)
        {
            Pacijent pacijent = new Pacijent();

            try
            {
                if (!textBoxPac.Text.Trim().Equals(""))
                {
                    int pacijentovID = Convert.ToInt32(textBoxPac.Text);
                    if (pacijent.obrisiPacijenta(pacijentovID))
                    {
                        MessageBox.Show("Pacijent je obrisan", "Obrisi pacijenta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Greska", "Obrisi pacijenta", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                else
                {
                    MessageBox.Show("Pacijent nije odabran", "Obrisi pacijenta", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Obrisi pacijenta", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void buttonOdaberiPac_Click(object sender, EventArgs e)
        {
            //prikazi formu za izbor pacijenta koga zelimo
            Izaberi_Pacijenta izaberiPac = new Izaberi_Pacijenta();
            izaberiPac.ShowDialog(this);

            try
            {
                //pacijentov ID
                int pacijentovID = Convert.ToInt32(izaberiPac.dataGridView1.CurrentRow.Cells[0].Value.ToString());

                textBoxPac.Text = pacijentovID.ToString();
                
            }
            catch
            {

            }
        }

        private void buttonPokaziListuPac_Click(object sender, EventArgs e)
        {
            Cela_Lista_Pacijenata celaListaPac = new Cela_Lista_Pacijenata();
            celaListaPac.Show(this);
        }
    }
}
