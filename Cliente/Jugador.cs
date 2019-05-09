using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    class Jugador
    {

        string NombreUsuario {
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

        bool Activo
        {
            get { return Activo; }
            set { Activo = value; }
        }

        // Constructor que no toma argumentos:
        public Jugador()
        {
            NombreUsuario = "";
            Password = "";
            NumJugador = 0;
            CantFichas = 0;
            ApuestaActual = 0;
            Activo = false;
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

    }
}
