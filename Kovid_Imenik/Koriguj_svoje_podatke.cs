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
    public partial class Koriguj_svoje_podatke : Form
    {
        public Koriguj_svoje_podatke()
        {
            InitializeComponent();
        }

        Korisnik korisnik = new Korisnik();
        private void Koriguj_svoje_podatke_Load(object sender, EventArgs e)
        {
            //prikazi podatke korisnika
            DataTable table = korisnik.dodajKorisnikaPomocuID(Uopstenje.UopsteniKorisnickiID);
            textBoxImeKor.Text = table.Rows[0][1].ToString();
            textBoxPrezimeKor.Text = table.Rows[0][2].ToString();
            textBoxKorImeKor.Text = table.Rows[0][3].ToString();
            textBoxSifraKor.Text = table.Rows[0][4].ToString();
            textBoxSifraBolKor.Text = table.Rows[0][5].ToString();
        }

        private void buttonZatvori_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonMinimiziraj_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        //
        private void buttonKorigujKor_Click(object sender, EventArgs e)
        {
            Moja_Baza_Podataka bp = new Moja_Baza_Podataka();

            int idKorisnika = Uopstenje.UopsteniKorisnickiID;// ostani prijavljen u korisnicki id
            string Ime = textBoxImeKor.Text;
            string Prezime = textBoxPrezimeKor.Text;
            string Sifra = textBoxSifraKor.Text;
            string SifraBol = textBoxSifraBolKor.Text;
            string KorIme = textBoxKorImeKor.Text;

            if (KorIme.Trim().Equals(" ") || Sifra.Trim().Equals("")) //proveri prazna polja
            {
                MessageBox.Show("Potrebna polja: Korisnicko ime i Sifra", "Informacija o korigovanju", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (korisnik.korImePostojanje(KorIme, "Koriguj", idKorisnika))//proveri da li korIme vec postoji 
                {
                    MessageBox.Show("Ovo korisnicko ime vec postoji probajte drugo", "Nekorektno korisnicko ime",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                else
                if (korisnik.korigujKorisnika(idKorisnika, Ime, Prezime, KorIme, Sifra, SifraBol))
                {
                    MessageBox.Show("Tvoji podaci su korigovani", "Informacije o korigovanju", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Greska", "Informacije o korigovanju", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
