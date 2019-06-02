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
        public List<Carta> Mesa { get; set; }

        [JsonProperty]
        public List<Jugador> Jugadores { get; set; }

        [JsonProperty]
        public int ApuestaAlta { get; set; }

        [JsonProperty]
        public int ApuestaMinima { get; set; }

        [JsonProperty]
        public int Bote { get; set; }

        public Juego()
        {
            Mazo = new Stack<Carta>();
            Mesa = new List<Carta>();
            Jugadores = new List<Jugador>();
        }

    }
}
