using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Models
{
    class Casa
    {
        private Stack<Carta> Mazo { get; set; }

        private Carta[] Mesa { get; set; }

        private Jugador[] Jugadores { get; set; }

        private int ApuestaAlta { get; set; }

        private int ApuestaMinima { get; set; }

        private int ApuestaTotal { get; set; }

        Casa()
        {
            LlenarMazo();
        }

        void LlenarMazo()
        {
            string[] leyendas = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
            List<Carta> cartas = new List<Carta>();


            for (int i = 0; i < 4; i++)
            {
                for (int p = 0; p < 13; p++)
                {
                    cartas.Add(new Carta(leyendas[p], i));
                }
            }
            Barajar(cartas);
            Mazo = new Stack<Carta>(cartas);
        }

        public static void Barajar(List<Carta> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Carta value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

        }

        static void Main(string[] args)
        {
            Casa casa = new Casa();

        }

    }
}
