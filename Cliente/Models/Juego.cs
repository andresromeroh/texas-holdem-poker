using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cliente.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    class Juego
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
            Mesa = new List<Carta>();
            Jugadores = new List<Jugador>();
        }

    }
}
