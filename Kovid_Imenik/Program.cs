using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kovid_Imenik
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new Registracija_Logovanje());

            //onemoguci glavni prozor ako u prozoru logovanja dialogrezul= OK
            Registracija_Logovanje prozorPrijava = new Registracija_Logovanje();

            if (prozorPrijava.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new GlavniProzor());
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
