using System.Collections.Generic;
using Newtonsoft.Json;

namespace Cliente.Models
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
            Mazo = new Stack<Carta>();
            CartasComunes = new List<Carta>();
            Jugadores = new List<Jugador>();
            Turno = 0;
            Ronda = 0;
            Informacion = "";
        }

    }
}
