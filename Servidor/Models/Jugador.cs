using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    class Jugador
    {
        [JsonProperty]
        string NombreUsuario
        {
            get { return NombreUsuario; }
            set { NombreUsuario = value; }
        }

        [JsonProperty]
        string Password
        {
            get { return Password; }
            set { Password = value; }
        }

        [JsonProperty]
        int NumJugador
        {
            get { return NumJugador; }
            set { NumJugador = value; }
        }

        [JsonProperty]
        int CantFichas
        {
            get { return CantFichas; }
            set { CantFichas = value; }
        }

        [JsonProperty]
        int ApuestaActual
        {
            get { return ApuestaActual; }
            set { ApuestaActual = value; }
        }

        [JsonProperty]
        int Role
        {
            get { return Role; }
            set { Role = value; }
        }

        [JsonProperty]
        bool Activo
        {
            get { return Activo; }
            set { Activo = value; }
        }

        // Tipos de roles:
        public static int REGULAR = 0;
        public static int DEALER = 1;
        public static int APUESTA_ALTA = 2;
        public static int APUESTA_BAJA = 3;


        // Constructor que no toma argumentos:
        public Jugador()
        {
        }

        // Constructor que toma argumentos:
        public Jugador(string nombreUsuario, string password, int numJugador, int cantFichas, int apuestaActual, bool activo)
        {
            NombreUsuario = nombreUsuario;
            Password = password;
            NumJugador = numJugador;
            CantFichas = cantFichas;
            ApuestaActual = apuestaActual;
            Activo = activo;
        }

    }
}
