using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor
{
    static class Sala
    {
        public static Mesa mesa = null;

        public static Mesa Mesa
        {
            get { return Mesa; }
        }

        public static void Init()
        {
            mesa = new Mesa(4, 100); // Mesa de 4 jugadores con apuesta minima de $100
        }
    }
}
