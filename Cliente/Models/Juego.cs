using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cliente.Models
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
            Mesa = new List<Carta>();
            Jugadores = new List<Jugador>();
        }

    }
}
