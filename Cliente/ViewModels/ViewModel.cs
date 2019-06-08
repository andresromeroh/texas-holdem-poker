using Cliente.Models;
using Cliente.Services;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Juego Juego { get; set; }
        public Jugador Jugador { get; set; }
        public string Carta1 { get; set; }
        public string Carta2 { get; set; }
        public string CartaFlop1 { get; set; }
        public string CartaFlop2 { get; set; }
        public string CartaFlop3 { get; set; }
        public string CartaTurn { get; set; }
        public string CartaRiver { get; set; }

        protected void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ViewModel()
        {
            Juego = JsonConvert.DeserializeObject<Juego>(ClienteTCP.Read());
            OnPropertyChange("Juego");
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
            Carta1 = "/Resources/Images/Cards/" + Jugador.Mano[0].TipoPalo + Jugador.Mano[0].Leyenda + ".png";
            Carta2 = "/Resources/Images/Cards/" + Jugador.Mano[1].TipoPalo + Jugador.Mano[1].Leyenda + ".png";
            OnPropertyChange("Carta1");
            OnPropertyChange("Carta2");
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
                return;
            }
            else
            {
                if (Jugador.Role.Equals(Jugador.APUESTA_BAJA))
                {
                    Jugador.ApuestaActual += (Juego.ApuestaAlta - Juego.ApuestaMinima);
                    Jugador.CantFichas -= (Juego.ApuestaAlta - Juego.ApuestaMinima);
                    Juego.Bote += (Juego.ApuestaAlta - Juego.ApuestaMinima);
                }
                else
                {
                    Jugador.ApuestaActual = Juego.ApuestaAlta;
                    Jugador.CantFichas -= Jugador.ApuestaActual;
                    Juego.Bote += Jugador.ApuestaActual;
                }
            }
            OnPropertyChange("Jugador");
        }

        public void RegularCall()
        {
            int max = ObtenerApuestaMax();

            Jugador.ApuestaActual += (max - Jugador.ApuestaActual);
            Jugador.CantFichas -= (max - Jugador.ApuestaActual);
            Juego.Bote += (max - Jugador.ApuestaActual);

            OnPropertyChange("Jugador");
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

        public void Actualizar()
        {
            Console.WriteLine("Actualizando");
            Juego = JsonConvert.DeserializeObject<Juego>(ClienteTCP.Read());
            OnPropertyChange("Juego");

            switch (Juego.Ronda)
            {
                case 1:
                    ObtenerFlop();
                    break;

                case 2:
                    ObtenerTurn();
                    break;

                case 3:
                    ObtenerRiver();
                    break;

                default:
                    break;
            }
        }

    }
}
