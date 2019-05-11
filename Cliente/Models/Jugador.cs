using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    class Jugador
    {

        string NombreUsuario
        {
            get { return NombreUsuario; }
            set { NombreUsuario = value; }
        }

        string Password
        {
            get { return Password; }
            set { Password = value; }
        }

        int NumJugador
        {
            get { return NumJugador; }
            set { NumJugador = value; }
        }

        int CantFichas
        {
            get { return CantFichas; }
            set { CantFichas = value; }
        }

        int ApuestaActual
        {
            get { return ApuestaActual; }
            set { ApuestaActual = value; }
        }

        int role
        {
            get { return role; }
            set { role = value; }
        }

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
        public Jugador(string nombreUsuario, string password, int numJugador,int cantFichas, int apuestaActual, bool activo)
        {
            NombreUsuario = nombreUsuario;
            Password = password;
            NumJugador = numJugador;
            CantFichas = cantFichas;
            ApuestaActual = apuestaActual;
            Activo = activo;
        }

        public override bool Equals(object obj)
        {
            return obj is Jugador jugador &&
                   NombreUsuario == jugador.NombreUsuario &&
                   Password == jugador.Password &&
                   NumJugador == jugador.NumJugador &&
                   CantFichas == jugador.CantFichas &&
                   ApuestaActual == jugador.ApuestaActual &&
                   role == jugador.role &&
                   Activo == jugador.Activo;
        }
    }
}
