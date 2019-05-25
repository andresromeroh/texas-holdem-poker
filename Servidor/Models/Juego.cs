using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Servidor.Models;

namespace Cliente.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Juego
    {
        [JsonProperty]
        private Stack<Carta> Mazo { get; set; }

        [JsonProperty]
        private List<Carta> Mesa { get; set; }

        [JsonProperty]
        private List<Jugador> Jugadores { get; set; }

        [JsonProperty]
        private int ApuestaAlta { get; set; }

        [JsonProperty]
        private int ApuestaMinima { get; set; }

        [JsonProperty]
        private int ApuestaTotal { get; set; }

        public Juego()
        {
            LlenarMazo();
            Mesa = new List<Carta>();
            Jugadores = new List<Jugador>();
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

        public void Flop()
        {
            for (int i = 0; i < 3; i++)
                Mesa[i] = Mazo.Pop();
        }

        public void River()
        {
            for (int i = 0; i < Mesa.Count; i++)
            {
                if (Mesa[i] == null)
                {
                    Mesa[i] = Mazo.Pop();
                }
            }
        }

        public void Turn()
        {
            for (int i = 0; i < Mesa.Count; i++)
            {
                if (Mesa[i] == null)
                {
                    Mesa[i] = Mazo.Pop();
                }
            }
        }

    }
}
