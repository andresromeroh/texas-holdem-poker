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
        public Juego Juego;
        public string Carta1 { get; set; }
        public string Carta2 { get; set; }

        protected void OnPropertyChange(string propertyName)
        {
            Console.WriteLine("PROPERTY CHANGED()\n");
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ViewModel()
        {
            Juego = JsonConvert.DeserializeObject<Juego>(ClienteTCP.Read());
        }

        public void ObtenerMano(string nombre)
        {
            foreach (Jugador jugador in Juego.Jugadores)
            {
                if (jugador.NombreUsuario.Equals(ClienteTCP.Name()))
                {
                    Carta1 = "/Resources/Images/Cards/" + jugador.Mano[0].TipoPalo + jugador.Mano[0].Leyenda + ".png";
                    Carta2 = "/Resources/Images/Cards/" + jugador.Mano[1].TipoPalo + jugador.Mano[1].Leyenda + ".png";
                    OnPropertyChange("Carta1");
                    OnPropertyChange("Carta2");
                }
            }
        }

    }
}
