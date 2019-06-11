using Cliente.Models;
using Cliente.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
namespace Cliente
{
    class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Juego Juego { get; set; }
        public Jugador Jugador { get; set; }
        public string CartaMano1 { get; set; }
        public string CartaMano2 { get; set; }
        public string CartaFlop1 { get; set; }
        public string CartaFlop2 { get; set; }
        public string CartaFlop3 { get; set; }
        public string CartaTurn { get; set; }
        public string CartaRiver { get; set; }
        public bool Jugando { get; set; }

        protected void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ViewModel()
        {
            // Constructor
        }

        public List<Jugador> ObtenerJugadores()
        {
            return Juego.Jugadores;
        }

        public void ActualizarInfoJugador()
        {
            foreach (Jugador jugador in Juego.Jugadores)
            {
                if (jugador.NombreUsuario.Equals(ClienteTCP.Name()))
                {
                    Jugador = jugador;
                    OnPropertyChange("Jugador");
                }
            }
        }

        public void ObtenerMano(string nombre)
        {
            if (Jugador.Activo)
            {
                CartaMano1 = "/Resources/Images/Cards/" + Jugador.Mano[0].TipoPalo + Jugador.Mano[0].Leyenda + ".png";
                CartaMano2 = "/Resources/Images/Cards/" + Jugador.Mano[1].TipoPalo + Jugador.Mano[1].Leyenda + ".png";
                OnPropertyChange("CartaMano1");
                OnPropertyChange("CartaMano2");
            }
        }

        public void ObtenerFlop()
        {
            CartaFlop1 = "/Resources/Images/Cards/" + Juego.CartasComunes[0].TipoPalo + Juego.CartasComunes[0].Leyenda + ".png";
            CartaFlop2 = "/Resources/Images/Cards/" + Juego.CartasComunes[1].TipoPalo + Juego.CartasComunes[1].Leyenda + ".png";
            CartaFlop3 = "/Resources/Images/Cards/" + Juego.CartasComunes[2].TipoPalo + Juego.CartasComunes[2].Leyenda + ".png";
            OnPropertyChange("CartaFlop1");
            OnPropertyChange("CartaFlop2");
            OnPropertyChange("CartaFlop3");
        }

        public void ObtenerTurn()
        {
            CartaTurn = "/Resources/Images/Cards/" + Juego.CartasComunes[3].TipoPalo + Juego.CartasComunes[3].Leyenda + ".png";
            OnPropertyChange("CartaTurn");
        }

        public void ObtenerRiver()
        {
            CartaRiver = "/Resources/Images/Cards/" + Juego.CartasComunes[4].TipoPalo + Juego.CartasComunes[4].Leyenda + ".png";
            OnPropertyChange("CartaRiver");
        }

        public void FlopCall()
        {
            if (Jugador.Role.Equals(Jugador.APUESTA_ALTA))
            {
                ActualizarInformacion(Jugador.NombreUsuario + " ha decidido pasar...\n");
                return;
            }
            else
            {
                if (Jugador.Role.Equals(Jugador.APUESTA_BAJA))
                {
                    Jugador.ApuestaActual += (Juego.ApuestaAlta - Juego.ApuestaMinima);
                    Jugador.CantFichas -= (Juego.ApuestaAlta - Juego.ApuestaMinima);
                    Juego.Bote += (Juego.ApuestaAlta - Juego.ApuestaMinima);
                    ActualizarInformacion(Jugador.NombreUsuario + " ha igualado la apuesta...\n");
                }
                else
                {
                    Jugador.ApuestaActual = Juego.ApuestaAlta;
                    Jugador.CantFichas -= Jugador.ApuestaActual;
                    Juego.Bote += Jugador.ApuestaActual;
                    ActualizarInformacion(Jugador.NombreUsuario + " ha igualado la apuesta...\n");
                }
            }
        }

        public void RegularCall()
        {
            int max = ObtenerApuestaMax();
            int diferencia = 0;

            if (max == Jugador.ApuestaActual)
            {
                Jugador.ApuestaActual += 100;
                Jugador.CantFichas -= 100;
                Juego.Bote += 100;
                ActualizarInformacion(Jugador.NombreUsuario + " ha decidido pasar...\n");
            }
            else
            {
                if (max > Jugador.ApuestaActual)
                {
                    diferencia = (max - Jugador.ApuestaActual);
                    Jugador.ApuestaActual += diferencia;
                    Jugador.CantFichas = Jugador.CantFichas - diferencia;
                    Juego.Bote += diferencia;
                    ActualizarInformacion(Jugador.NombreUsuario + " ha igualado la apuesta...\n");
                }
                else
                {
                    return;
                }
            }
        }

        public void Raise(int cantApuesta)
        {
            Jugador.ApuestaActual += cantApuesta;
            Jugador.CantFichas -= cantApuesta;
            Juego.Bote += cantApuesta;
            ActualizarInformacion(Jugador.NombreUsuario + " ha subido la apuesta a " + cantApuesta + "\n");
        }
        public void check() {
            if (Jugador.Role == Jugador.APUESTA_ALTA)
            {
                Jugador.ApuestaActual = 0;
                ActualizarInformacion(Jugador.NombreUsuario + "a decidido no apostar\n");
            }
            else {
                string caption = "Apueste!";
                string message = Jugador.NombreUsuario +" usted no tiene la apuesta alta , apueste o retirese!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;

                //Muestra el mensaje
                System.Windows.Forms.MessageBox.Show(message, caption, buttons);
            }
        }

        public void Fold()
        {
            Jugador.Activo = false;
            Jugador.Mano[0] = null;
            Jugador.Mano[1] = null;
            CartaMano1 = "/Resources/Images/Cards/green_back.png";
            CartaMano2 = "/Resources/Images/Cards/green_back.png";
            OnPropertyChange("CartaMano1");
            OnPropertyChange("CartaMano2");
            ActualizarInformacion(Jugador.NombreUsuario + " ha decidido no ir!\n");
        }

        public int ObtenerApuestaMax()
        {
            int max = 0;

            foreach (Jugador jugador in Juego.Jugadores)
            {
                if (jugador.ApuestaActual > max)
                {
                    max = jugador.ApuestaActual;
                }
            }

            return max;
        }

        public void RestablecerCartas()
        {
            CartaMano1 = "/Resources/Images/Cards/green_back.png";
            CartaMano2 = "/Resources/Images/Cards/green_back.png";
            CartaFlop1 = "/Resources/Images/Cards/red_back.png";
            CartaFlop2 = "/Resources/Images/Cards/red_back.png";
            CartaFlop3 = "/Resources/Images/Cards/red_back.png";
            CartaTurn = "/Resources/Images/Cards/red_back.png";
            CartaRiver = "/Resources/Images/Cards/red_back.png";
            OnPropertyChange("CartaMano1");
            OnPropertyChange("CartaMano2");
            OnPropertyChange("CartaFlop1");
            OnPropertyChange("CartaFlop2");
            OnPropertyChange("CartaFlop3");
            OnPropertyChange("CartaTurn");
            OnPropertyChange("CartaRiver");
        }

        public void ActualizarInformacion(string mensaje)
        {
            Juego.Informacion += mensaje;
            OnPropertyChange("Juego");
        }

        public void ActualizarTurno()
        {
            if (Juego.Turno == Jugador.NumJugador)
            {
                Jugando = true;
                OnPropertyChange("Jugando");
            }
            else
            {
                Jugando = false;
                OnPropertyChange("Jugando");
            }
        }

        public void Actualizar()
        {
            Juego = JsonConvert.DeserializeObject<Juego>(ClienteTCP.Read());
            OnPropertyChange("Juego");

            switch (Juego.Ronda)
            {
                case 0:
                    RestablecerCartas();
                    break;

                case 1:
                    ActualizarInfoJugador();
                    ObtenerMano(ClienteTCP.Name());
                    break;

                case 2:
                    ObtenerFlop();
                    break;

                case 3:
                    ObtenerTurn();
                    break;

                case 4:
                    ObtenerRiver();
                    break;

                default:
                    break;
            }
        }

    }
}
