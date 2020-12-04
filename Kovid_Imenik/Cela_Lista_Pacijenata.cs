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
using Microsoft.Build.Tasks;
using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient;



namespace Kovid_Imenik
{
    public partial class Cela_Lista_Pacijenata : Form
    {
        public Cela_Lista_Pacijenata()
        {
            InitializeComponent();
        }

        private void buttonZatvori_Click(object sender, EventArgs e)
        {
            Close();
        }

        Moja_Baza_Podataka bp = new Moja_Baza_Podataka();
        private void buttonMinimiziraj_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Cela_Lista_Pacijenata_Load(object sender, EventArgs e)
        {
            //povezujemo datagridview sa pacijentima
            Pacijent pacijent = new Pacijent();
            MySqlCommand command = new MySqlCommand("SELECT *FROM tabela.pacijenti ");
            command.Parameters.Add("", MySqlDbType.Int32).Value = Uopstenje.UopsteniKorisnickiID;
            dataGridView1.DataSource = pacijent.izaberiPacijentaLista(command);

            for (int i=0; i < dataGridView1.Rows.Count; i++)
            {
                if (parna(i))
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Azure;
                }
            }
        }

        //zelim da obojim svaku drugu liniju u listi

        public bool parna(int v)
        {
            //vraca true ako je v = 2 4 6... u suprotnom false
            return v % 2 != 0;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (parna(i))
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Azure;
                }
            }
        }

        private void Statistike_Click(object sender, EventArgs e)
        {
            ExportToPDF();
        }

        private void ExportToPDF()
        {
            Moja_Baza_Podataka bp = new Moja_Baza_Podataka();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            DataTable table = new DataTable();

            MySqlCommand command = new MySqlCommand(" select count(Oporavljen) as Total from tabela.pacijenti where Oporavljen = 'Da';", bp.getConnection);


            //postavljamo SQL izraz koji se koristi za odabir zapisa u izvoru podataka
            adapter.SelectCommand = command;

            //u table se upisuje ukupan broj oporavljenih
            adapter.Fill(table);
            /*string deviceInfo = ""; //za podesavanja sirine i visine itd, koristimo po difoltu
            string[] streamIds;
            Microsoft.Reporting.WinForms.Warning[] warnings;

            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = "Report1.rdlc";
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet", table));
            viewer.RefreshReport();

            var bytes = viewer.LocalReport.Render("PDF", deviceInfo, out mimeType, out encoding, out extension,
                out streamIds, out warnings
                );

            string fileName = @"D:\Statistike.pdf";
            File.WriteAllBytes(fileName, bytes);
            System.Diagnostics.Process.Start(fileName);*/
        }

    }
}
