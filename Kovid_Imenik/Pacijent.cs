using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using MySql.Data.MySqlClient;

namespace Kovid_Imenik
{
    class Pacijent
    {
        Moja_Baza_Podataka bp = new Moja_Baza_Podataka();

        //ubaci novog pacijenta
        public bool ubaciPacijenta(string Ime, string Prezime, string JMBG, string BrTel, string LBO, string PoslednjiTest, string RezultatTest, string Oporavljen, string PodlegaoBolesti, string BezSimptoma, string Dijabetes, string KVProblemi, string PlucneBolesti)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO tabela.pacijenti(Ime, Prezime, JMBG, BrTel, LBO, PoslednjiTest, RezultatTesta, Oporavljen, PodlegaoBolesti, BezSimptoma, Dijabetes, KVProblemi, PlucneBolesti) VALUES (@Ime, @Prezime, @JMBG, @BrTel, @LBO, @PoslednjiTest, @RezultatTesta, @Oporavljen, @PodlegaoBolesti, @BezSimptoma, @Dijabetes, @KVProblemi, @PlucneBolesti)", bp.getConnection);

            
            command.Parameters.Add("@Ime", MySqlDbType.VarChar).Value = Ime;
            command.Parameters.Add("@Prezime", MySqlDbType.VarChar).Value = Prezime;
            command.Parameters.Add("@JMBG", MySqlDbType.VarChar).Value = JMBG;
            command.Parameters.Add("@BrTel", MySqlDbType.VarChar).Value = BrTel; ;
            command.Parameters.Add("@LBO", MySqlDbType.VarChar).Value = LBO;
            command.Parameters.Add("@PoslednjiTest", MySqlDbType.VarChar).Value = PoslednjiTest;
            command.Parameters.Add("@RezultatTesta", MySqlDbType.VarChar).Value = RezultatTest;
            command.Parameters.Add("@Oporavljen", MySqlDbType.VarChar).Value = Oporavljen;
            command.Parameters.Add("@PodlegaoBolesti", MySqlDbType.VarChar).Value = PodlegaoBolesti;
            command.Parameters.Add("@BezSimptoma", MySqlDbType.VarChar).Value = BezSimptoma;
            command.Parameters.Add("@Dijabetes", MySqlDbType.VarChar).Value = Dijabetes;
            command.Parameters.Add("@KVProblemi", MySqlDbType.VarChar).Value = KVProblemi;
            command.Parameters.Add("@PlucneBolesti", MySqlDbType.VarChar).Value = PlucneBolesti;
            

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

        //koriguj pacijenta
        public bool korigujPacijenta(int ID, string Ime, string Prezime, string JMBG, string BrTel, string LBO, string PoslednjiTest, string RezultatTest, string Oporavljen, string PodlegaoBolesti, string BezSimptoma, string Dijabetes, string KVProblemi, string PlucneBolesti)
        {
            MySqlCommand command = new MySqlCommand("UPDATE tabela.pacijenti SET Ime=@Ime , Prezime=@Prezime, JMBG=@JMBG, BrTel=@BrTel, LBO=@LBO, PoslednjiTest=@PoslednjiTest, RezultatTesta=@RezultatTesta, Oporavljen=@Oporavljen, PodlegaoBolesti=@PodlegaoBolesti, BezSimptoma=BezSimptoma, Dijabetes=@Dijabetes, KVProblemi=@KVProblemi, PlucneBolesti=@PlucneBolesti WHERE ID=@ID", bp.getConnection);

            // korisnicki id je vec podesen sa pacijentom
            
            command.Parameters.Add("@Ime", MySqlDbType.VarChar).Value = Ime;
            command.Parameters.Add("@Prezime", MySqlDbType.VarChar).Value = Prezime;
            command.Parameters.Add("@JMBG", MySqlDbType.VarChar).Value = JMBG;
            command.Parameters.Add("@BrTel", MySqlDbType.VarChar).Value = BrTel; ;
            command.Parameters.Add("@LBO", MySqlDbType.VarChar).Value = LBO;
            command.Parameters.Add("@PoslednjiTest", MySqlDbType.VarChar).Value = PoslednjiTest;
            command.Parameters.Add("@RezultatTesta", MySqlDbType.VarChar).Value = RezultatTest;
            command.Parameters.Add("@Oporavljen", MySqlDbType.VarChar).Value = Oporavljen;
            command.Parameters.Add("@PodlegaoBolesti", MySqlDbType.VarChar).Value = PodlegaoBolesti;
            command.Parameters.Add("@BezSimptoma", MySqlDbType.VarChar).Value = BezSimptoma;
            command.Parameters.Add("@Dijabetes", MySqlDbType.VarChar).Value = Dijabetes;
            command.Parameters.Add("@KVProblemi", MySqlDbType.VarChar).Value = KVProblemi;
            command.Parameters.Add("@PlucneBolesti", MySqlDbType.VarChar).Value = PlucneBolesti;
            command.Parameters.Add("@ID", MySqlDbType.Int32).Value = ID;

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
        //funkcija koja vraca listu pacijenata u zavisnosti od zadate komande
        public DataTable izaberiPacijentaLista(MySqlCommand command)
        {
            command.Connection = bp.getConnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        //funkcija za nalazenje pacijenta pomocu njgovog ID-a
        public DataTable nadjiPacPomocuID(int pacID)
        {
            MySqlCommand command = new MySqlCommand("SELECT ID, Ime, Prezime, JMBG, BrTel, LBO, PoslednjiTest, RezultatTesta, Oporavljen, PodlegaoBolesti, BezSimptoma, Dijabetes, KVProblemi, PlucneBolesti FROM tabela.pacijenti WHERE ID=@ID", bp.getConnection);
            command.Parameters.Add("@ID", MySqlDbType.Int32).Value=pacID;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        //funkcija koja brise pacijenta pomocu ID-a
        public bool obrisiPacijenta(int pacID)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM tabela.pacijenti WHERE ID=@ID", bp.getConnection);

            command.Parameters.Add("@ID", MySqlDbType.Int32).Value = pacID;

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
