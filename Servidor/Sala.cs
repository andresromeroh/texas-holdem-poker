using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor
{
    static class Sala
    {
        public static Mesa Mesa = null;
        public static void Init()
        {
            Mesa = new Mesa(4, 100); // Mesa de 4 jugadores con apuesta minima de $100
        }
    }
}
