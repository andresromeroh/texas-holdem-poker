using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Jugador
    {
        [JsonProperty]
        public string NombreUsuario { get; set; }

        [JsonProperty]
        public string Password;

        [JsonProperty]
        public int NumJugador { get; set; }

        [JsonProperty]
        public Carta[] Mano;

        [JsonProperty]
        public int CantFichas { get; set; }

        [JsonProperty]
        public int ApuestaActual { get; set; }

        [JsonProperty]
        public string Estado { get; set; }

        [JsonProperty]
        public string Role { get; set; }

        [JsonProperty]
        public bool Activo { get; set; }

        [JsonProperty]
        public int PuntajeMano;

        // Tipos de estados:
        public static string ESPERANDO = "ESPERANDO";
        public static string JUGANDO = "JUGANDO";

        // Tipos de roles:
        public static string REGULAR = "REGULAR";
        public static string DEALER = "DEALER";
        public static string APUESTA_ALTA = "APUESTA ALTA";
        public static string APUESTA_BAJA = "APUESTA BAJA";


        // Constructor que no toma argumentos:
        public Jugador()
        {
            Mano = new Carta[2];
        }

        // Constructor que toma argumentos:
        public Jugador(string nombreUsuario, string password, int cantFichas, bool activo)
        {
            NombreUsuario = nombreUsuario;
            Password = password;
            CantFichas = cantFichas;
            Activo = activo;
            Mano = new Carta[2];
        }

    }
}
