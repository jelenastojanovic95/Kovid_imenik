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
    public partial class Registracija_Logovanje : Form
    {
        public Registracija_Logovanje()
        {
            InitializeComponent();
        }

        private void Registracija_Logovanje_Load(object sender, EventArgs e)
        {

        }
        //dugme prijavi se
        private void buttonPrijaviSe_Click(object sender, EventArgs e)
        {
            Moja_Baza_Podataka bp = new Moja_Baza_Podataka();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            DataTable table = new DataTable();

            MySqlCommand command = new MySqlCommand(" SELECT * FROM tabela.korisnici WHERE KorIme = @KorIme AND Sifra = @Sifra",bp.getConnection);

            //uzimamo KorIme i sifru koje smo uneli pri prijavi
            command.Parameters.Add("@KorIme", MySqlDbType.VarChar).Value = textBoxKorisnickoIme.Text;
            command.Parameters.Add("@Sifra", MySqlDbType.VarChar).Value = textBoxSifra.Text;

            //postavljamo SQL izraz koji se koristi za odabir zapisa u izvoru podataka
            adapter.SelectCommand = command;

            //dodajemo nove redove u skupu podataka kako bi se podudarali sa onim u izvoru podataka pomocu DataTable
            //tj popunjavamo table sa svim korisnickim imenom i sifrom koju smo ucitali iz prozora pod uslovom da pripada bazi podataka
            //ako nema kor imena i sifre u bazi table je prazna
            adapter.Fill(table);

            if (verifFields("Prijavi se"))//provera praznog polja
            {
                if (table.Rows.Count > 0)// proveri da li ovaj korisnik postoji
                {
                    
                    //zelimo da prikazemo korisnikovo korisnicko ime u Glavnom prozoru i da prenesemo informaciju njegovog ID-a u sledeci prozor
                    // da bi smo to uradili potreban je korisnicki ID i napraviti ga globalnom opcijom i za drugi prozor
                    int korisnickiID = Convert.ToInt32(table.Rows[0][0].ToString());
                    Uopstenje.podesiUopsteniKorisnickiID(korisnickiID);

                    //pokazi formu glavne aplikacije
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Korisnicko ime ili sifra nisu tacni", "Greska u prijavi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Niste uneli korisnicko ime ili sifru", "Greska u prijavi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        //dugme registruj se
        private void buttonRegistrujSe_Click(object sender, EventArgs e)
        {
            string ime = textBoxIme.Text;
            string prezime = textBoxPrezime.Text;
            string korIme = textBoxKorisnickoImeR.Text;
            string sifra = textBoxSifraR.Text;
            string sifraBol = textBoxSifraBolnice.Text;

            Korisnik korisnik = new Korisnik();

            if (verifFields("Registruj se"))
            {
               //treba proveriti da li korIme vec postoji
               //treba ubaciti novog korisnika u bazu
               //kreiracemo klasu Korisnik
               if (!korisnik.korImePostojanje(korIme,"Registruj se"))//ako ne postoji
                {
                    if (korisnik.ubaciKorisnika(ime, prezime, korIme, sifra, sifraBol))
                    {
                        MessageBox.Show("Registracija je uspesno obavljena", "Registruj se", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Niste uspeli", "Registruj se", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
               else
                {
                    MessageBox.Show("Ovo korisnicko ime vec postoji probajte neko drugo", "Nekorektno korisnicko ime", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Registracija nije uspela ", "Registruj se", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //Idi na registraciju labela
        private void labelIdiNaRegistraciju_Click(object sender, EventArgs e)
        {
            timer1.Start();
            labelIdiNaRegistraciju.Enabled = false;
            labelIdiNaPrijavu.Enabled = false;
        }

        // kreiramo funkciju za proveru praznih fajlova
        public bool verifFields(string operation)
        {
            bool check = false;
            //ako je operacija registracija
            if (operation == "Registruj se")
            {
                
                if (textBoxKorisnickoImeR.Text.Trim().Equals("") || textBoxIme.Text.Trim().Equals("") || textBoxPrezime.Text.Trim().Equals("") ||  textBoxSifraR.Text.Trim().Equals("") || textBoxSifraBolnice.Text.Equals(""))
                {
                    check = false;
                }
                else
                {
                    check = true;
                }
            }//ako je operacija logovanje
            else if (operation == "Prijavi se")
            {
               if (textBoxKorisnickoIme.Text.Trim().Equals("") || textBoxSifra.Text.Trim().Equals(""))
                {
                    check = false;
                }
                else
                {
                    check = true;
                }
            }
            return check;
        }

        //idi na prijavu
        private void labelIdiNaPrijavu_Click(object sender, EventArgs e)
        {
            timer2.Start();
            labelIdiNaPrijavu.Enabled = false;
            labelIdiNaRegistraciju.Enabled = false;
        }

        private void buttonZatvori_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonMinimiziraj_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        //kada se tajmer pokrene pojavice se samo deo za registraciju
        private void timer1_Tick(object sender, EventArgs e)
        {
            //-401 je lokacija panela gde mozemo videti deo sa registracijom
            //401 je lokacija panela gde mozemo videti  deo sa prijavom
            //dakle panel treba da ide van svoje pocetne forme za 401
            //slicno kada zelimo da pokazemo deo sa logovanjem
            //potrebno nam je da lokacija X panela bude 0
            if (panel2.Location.X>-401)//ako je lokacija X koordinate na prijavi, dodeljujemo novu lokaciju
            {
                panel2.Location = new Point(panel2.Location.X - 10, panel2.Location.Y);
            }
            else
            {
                timer1.Stop();
                labelIdiNaPrijavu.Enabled = true;
                labelIdiNaRegistraciju.Enabled = true;
            }
        }
        //kada se tajmer2 pokrene pojavice se samo deo za prijavu
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (panel2.Location.X < 0)
            {
                panel2.Location = new Point(panel2.Location.X + 10, panel2.Location.Y);
            }
            else
            {
                timer2.Stop();
                labelIdiNaPrijavu.Enabled = true;
                labelIdiNaRegistraciju.Enabled = true;
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
