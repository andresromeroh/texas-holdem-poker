using System;

namespace Servidor
{
    static class Sala
    {
        public static Mesa Mesa = null;
        public static void Init()
        {
            // Mesa de 4 jugadores con apuesta minima de $50 y alta de $100
            Mesa = new Mesa(4, 50, 100);
        }
    }
}
