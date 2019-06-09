using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Servidor
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Juego
    {
        [JsonProperty]
        public Stack<Carta> Mazo { get; set; }

        [JsonProperty]
        public List<Carta> CartasComunes { get; set; }

        [JsonProperty]
        public List<Jugador> Jugadores { get; set; }

        [JsonProperty]
        public int ApuestaAlta { get; set; }

        [JsonProperty]
        public int ApuestaMinima { get; set; }

        [JsonProperty]
        public int Bote { get; set; }

        [JsonProperty]
        public int Turno { get; set; }

        [JsonProperty]
        public int Ronda { get; set; }

        [JsonProperty]
        public string Informacion { get; set; }

        public Juego()
        {
            LlenarMazo();
            CartasComunes = new List<Carta>();
            Jugadores = new List<Jugador>();
            Turno = 0;
            Ronda = 0;
            Informacion = "";
        }

        public void LlenarMazo()
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

        public void SacarFlop()
        {
            for (int i = 0; i < 3; i++)
            {
                CartasComunes.Add(Mazo.Pop());
            }

            Ronda = 2;
        }

        public void SacarTurn()
        {
            CartasComunes.Add(Mazo.Pop());
            Ronda = 3;
        }

        public void SacarRiver()
        {
            CartasComunes.Add(Mazo.Pop());
            Ronda = 4;
        }

        public void Repartir()
        {
            foreach (Jugador jugador in Jugadores)
            {
                jugador.Mano[0] = Mazo.Pop();
                jugador.Mano[1] = Mazo.Pop();
            }

            Ronda = 1;
        }

        public void ActualizarInformacion(string mensaje)
        {
            Informacion += mensaje;
            Console.WriteLine(mensaje);
        }

        public void DefinirApuestas() // Define los jugadores con apuestas y hace su resta
        {
            DefinirJugadorApuestaBaja();
            DefinirJugadorApuestaAlta();
        }

        public void DefinirJugadorApuestaBaja()
        {
            for (int i = 0; i < Jugadores.Count; i++)
            {
                if (Jugadores[i].Role.Equals(Jugador.APUESTA_BAJA))
                {
                    Jugadores[i].Role = Jugador.REGULAR;

                    if (i == (Jugadores.Count - 1)) // Si es el ultimo
                    {
                        Jugadores[0].Role = Jugador.APUESTA_BAJA;
                        Jugadores[0].ApuestaActual = ApuestaMinima;
                        Jugadores[0].CantFichas -= ApuestaMinima;
                    }
                }

                break;
            }
        }

        public void DefinirJugadorApuestaAlta()
        {
            for (int i = 0; i < Jugadores.Count; i++)
            {
                if (Jugadores[i].Role.Equals(Jugador.APUESTA_BAJA))
                {
                    if (i == (Jugadores.Count - 1)) // Si es el ultimo
                    {
                        Jugadores[0].Role = Jugador.APUESTA_ALTA;
                        Jugadores[0].ApuestaActual = ApuestaAlta;
                        Jugadores[0].CantFichas -= ApuestaAlta;
                    }
                    else
                    {
                        Jugadores[i + 1].Role = Jugador.APUESTA_ALTA;
                        Jugadores[0].ApuestaActual = ApuestaAlta;
                        Jugadores[0].CantFichas -= ApuestaAlta;
                    }
                }

                break;
            }
        }

        public void analizarMano()
        {
            foreach (Jugador jugador in Jugadores)
            {
                cartaAlta(jugador);
                par(jugador);
                doblePar(jugador);
                trio(jugador);
                escalera(jugador);
                color(jugador);
                fullHouse(jugador);
                poker(jugador);
                escaleraDeColor(jugador);
                escaleraReal(jugador);
            }
        }

        /// Estefany
        public void cartaAlta(Jugador jugador)
        {
            jugador.PuntajeMano = 1;
        }
        public void par(Jugador jugador)
        {
            jugador.PuntajeMano = 2;
        }
        public void doblePar(Jugador jugador)
        {
            jugador.PuntajeMano = 3;
        }
        public void trio(Jugador jugador)
        {
            jugador.PuntajeMano = 4;
        }
        public void escalera(Jugador jugador)
        {
            jugador.PuntajeMano = 5;
        }

        ///Ariel
        public void color(Jugador jugador)
        {
            jugador.PuntajeMano = 6;
        }
        public void fullHouse(Jugador jugador)
        {
            jugador.PuntajeMano = 7;
        }
        public void poker(Jugador jugador)
        {
            jugador.PuntajeMano = 8;
        }
        public void escaleraDeColor(Jugador jugador)
        {
            jugador.PuntajeMano = 9;
        }
        public void escaleraReal(Jugador jugador)
        {
            jugador.PuntajeMano = 10;
        }
    }
}
