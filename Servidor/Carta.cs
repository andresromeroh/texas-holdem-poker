using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Carta
    {
        [JsonProperty]
        string Leyenda { get; set; }

        [JsonProperty]
        int TipoPalo { get; set; }

        public static int ESPADAS = 0;
        public static int CORAZONES = 1;
        public static int DIAMANTES = 2;
        public static int TREBOLES = 3;


        // Constructor que no toma argumentos:
        public Carta()
        {
        }

        // Constructor que toma argumentos:
        public Carta(string leyenda, int tipoPalo)
        {
            Leyenda = leyenda;
            TipoPalo = tipoPalo;
        }

        public override string ToString()
        {
            return String.Format("Leyenda: {0}; TipoPalo: {1};", Leyenda, TipoPalo);
        }

    }
}
