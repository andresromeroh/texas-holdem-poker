using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    class Carta
    {
        string Leyenda
        {
            get { return Leyenda; }
            set { Leyenda = value; }
        }

        int TipoPalo
        {
            get { return TipoPalo; }
            set { TipoPalo = value; }
        }

        public static int ESPADAS = 1;
        public static int CORAZONES = 2;
        public static int DIAMANTES = 3;
        public static int TREBOLES = 14;


        // Constructor que no toma argumentos:
        public Carta()
        {
            Leyenda = "";
            TipoPalo = 0;
        }

        // Constructor que toma argumentos:
        public Carta(string leyanda, int tipoPalo)
        {
            Leyenda = leyanda;
            TipoPalo = tipoPalo;
        }

    }
}
