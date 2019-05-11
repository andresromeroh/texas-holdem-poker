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

        public static int ESPADAS = 0;
        public static int CORAZONES = 1;
        public static int DIAMANTES = 2;
        public static int TREBOLES = 3;


        // Constructor que no toma argumentos:
        public Carta()
        {
        }

        // Constructor que toma argumentos:
        public Carta(string leyanda, int tipoPalo)
        {
            Leyenda = leyanda;
            TipoPalo = tipoPalo;
        }

        public override bool Equals(object obj)
        {
            return obj is Carta carta &&
                   Leyenda == carta.Leyenda &&
                   TipoPalo == carta.TipoPalo;
        }

    }
}
