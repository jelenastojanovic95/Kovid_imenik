using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
// treba dodati mysql konektor
//da bi smo povezali nasu aplikaciju sa mysql bazom podataka
using MySql.Data.MySqlClient;

namespace Kovid_Imenik
{
    class Moja_Baza_Podataka
    {
        //uspostavljanje veze
        MySqlConnection con = new MySqlConnection("datasource=localhost;port=3306;username=root;password=0124129495jeka;database=tabela");
        
        //vracanje veze
        public MySqlConnection getConnection//da bi ostale klase mogle da je koriste
        {
            get
            {
                return con;
            }
        }
        //otvoriti vezu
        public void openConnection()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }
        //zatvoriti vezu
        public void closeConnection()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}
