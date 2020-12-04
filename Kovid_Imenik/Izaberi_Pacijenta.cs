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
    public partial class Izaberi_Pacijenta : Form
    {
        public Izaberi_Pacijenta()
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

        private void Izaberi_Pacijenta_Load(object sender, EventArgs e)
        {
            //prikazi listu pacijenata 
            MySqlCommand command = new MySqlCommand("SELECT *FROM tabela.pacijenti");
            Pacijent pacijent = new Pacijent();
            dataGridView1.DataSource = pacijent.izaberiPacijentaLista(command);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.Close();
        }
    }
}
