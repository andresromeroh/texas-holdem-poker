using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Jugador
    {
        [JsonProperty]
        public string NombreUsuario;

        [JsonProperty]
        public string Password;

        [JsonProperty]
        public int NumJugador;

        [JsonProperty]
        public Carta[] Mano;

        [JsonProperty]
        public int CantFichas;

        [JsonProperty]
        public int ApuestaActual;

        [JsonProperty]
        public int Role;

        [JsonProperty]
        public bool Activo;

        // Tipos de roles:
        public static int REGULAR = 0;
        public static int DEALER = 1;
        public static int APUESTA_ALTA = 2;
        public static int APUESTA_BAJA = 3;


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
