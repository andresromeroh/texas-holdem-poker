using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Servidor;

namespace Servidor
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Juego
    {
        [JsonProperty]
        public Stack<Carta> Mazo;

        [JsonProperty]
        public List<Carta> Mesa;

        [JsonProperty]
        public List<Jugador> Jugadores;

        [JsonProperty]
        public int ApuestaAlta;

        [JsonProperty]
        public int ApuestaMinima;

        [JsonProperty]
        public int ApuestaTotal;

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

        public void Repartir()
        {
            foreach (Jugador jugador in Jugadores)
            {
                jugador.Mano[0] = Mazo.Pop();
                jugador.Mano[1] = Mazo.Pop();
            }
        }

        public void Showdown()
        {

        }

        public void EarlyWin()
        {

        }

        public void PlayerMove(Cliente cliente)
        {

        }

        public void Collect()
        {

        }

        public void Deal()
        {
            foreach (Jugador jugador in Jugadores)
            {
                jugador.Mano = repartirJugador();
                //jugador.Writer.WriteLine("Pocket$0$" + jugador.Pocket[0]); Enviar las cartas solo al jugador correspondiente
            }
        }

        public Carta[] repartirJugador()
        {
            return null; //Aqui se requiere repartir 2 cartas del top del mazo
        }

    }
}
