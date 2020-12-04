using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kovid_Imenik
{
    class Uopstenje
    {
        //ova klasa ce napraviti prijavu u korisnickom ID-u globalno svuda u aplikaciji

        public static int UopsteniKorisnickiID { get; private set; }

        public static void podesiUopsteniKorisnickiID ( int korisnickiID)
        {
            UopsteniKorisnickiID = korisnickiID;
        }
    }
}
