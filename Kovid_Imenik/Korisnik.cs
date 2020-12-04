using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;

namespace Kovid_Imenik
{
    public class Korisnik
    {
        Moja_Baza_Podataka bp = new Moja_Baza_Podataka();

        //funkcija za proveru korImena
        public bool korImePostojanje(string KorIme, string operacija, int korId=0)
        {
            string query = "";

            if (operacija=="Registruj se")
            {
                //ako se novi korisnik zeli registrovati provericemo da li vec korIme postoji 
                query = "SELECT * FROM tabela.korisnici WHERE KorIme = @KorIme ";
            }
            else if (operacija== "Koriguj")
            {
                //proveri da li je korisnik ukucao korIme koje vec postoji a nije njegovo
                query = "SELECT * FROM tabela.korisnici WHERE KorIme = @KorIme";
            }
            MySqlCommand command = new MySqlCommand(query, bp.getConnection);
            command.Parameters.Add("@KorIme",MySqlDbType.VarChar).Value=KorIme;

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);

            DataTable table = new DataTable();
            adapter.Fill(table);

            //ako korisnik postoji vrati true
            if (table.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //ubaci novog korisnika
        public bool ubaciKorisnika(string Ime, string Prezime, string KorIme, string Sifra, string SifraBol)
        {
            string query = "INSERT INTO tabela.korisnici(Ime,Prezime,KorIme,Sifra,SifraBol) VALUES (@Ime,@Prezime,@KorIme,@Sifra,@SifraBol)";
            MySqlCommand command = new MySqlCommand(query, bp.getConnection);
            command.Parameters.Add("@Ime", MySqlDbType.VarChar).Value = Ime;
            command.Parameters.Add("@Prezime", MySqlDbType.VarChar).Value = Prezime;
            command.Parameters.Add("@KorIme", MySqlDbType.VarChar).Value = KorIme;
            command.Parameters.Add("@Sifra", MySqlDbType.VarChar).Value = Sifra;
            command.Parameters.Add("@SifraBol", MySqlDbType.VarChar).Value = SifraBol;

            bp.openConnection();

            if (command.ExecuteNonQuery() == 1)//ako je unet korisnik vratice 1, jer unosimo jednog po jednog korisnika
            {
                bp.closeConnection();
                return true;
            }
            else//u suprotnom vraca -1
            {
                bp.closeConnection();
                return false;
            }
        }
        //kreiram funkciju koja vraca podatke korisnika koristeci njegov ID
        public DataTable dodajKorisnikaPomocuID(Int32 korisnickiID)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            MySqlCommand command = new MySqlCommand("SELECT * FROM tabela.korisnici WHERE ID = @ID",bp.getConnection);

            command.Parameters.Add("@ID", MySqlDbType.UInt32).Value = korisnickiID;

            adapter.SelectCommand = command;
            //popunjavamo table korisnikom sa odgovarajucim ID-om
            adapter.Fill(table);

            return table;
        }

        //kreiramo funkciju za korigovanje podataka, tj. korigujemo postojeceg korisnika

        public bool korigujKorisnika(int ID, string Ime, string Prezime, string KorIme, string Sifra, string SifraBol)
        {
            MySqlCommand command = new MySqlCommand("UPDATE tabela.korisnici SET Ime=@Ime, Prezime=@Prezime, KorIme=@KorIme, Sifra=@Sifra, SifraBol=@SifraBol WHERE ID=@ID", bp.getConnection);

            command.Parameters.Add("@ID", MySqlDbType.Int32).Value = ID;
            command.Parameters.Add("@Ime", MySqlDbType.VarChar).Value = Ime;
            command.Parameters.Add("@Prezime", MySqlDbType.VarChar).Value = Prezime;
            command.Parameters.Add("@KorIme", MySqlDbType.VarChar).Value = KorIme;
            command.Parameters.Add("@Sifra", MySqlDbType.VarChar).Value = Sifra;
            command.Parameters.Add("@SifraBol", MySqlDbType.VarChar).Value = SifraBol;
            

            bp.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                bp.closeConnection();
                return true;
            }
            else
            {
                bp.closeConnection();
                return false;
            }
        }
    }
}
