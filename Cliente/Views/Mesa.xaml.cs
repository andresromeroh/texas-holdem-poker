using Cliente.Models;
using Cliente.Services;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Windows;

namespace Cliente
{
    public partial class Mesa : Window
    {
        private readonly ViewModel ViewModel;

        public Mesa()
        {
            InitializeComponent();
            ViewModel = new ViewModel();
            DataContext = ViewModel;
            ViewModel.ActualizarInfoJugador();
            ViewModel.ObtenerMano(ClienteTCP.Name());

            Thread update = new Thread(Escuchar);
            update.Start();
        }

        private void Call(object sender, RoutedEventArgs e)
        {
            if (ViewModel.Juego.Ronda == 0)
            {
                ViewModel.FlopCall();
            }
            else
            {
                ViewModel.RegularCall();
            }
            ClienteTCP.Write(JsonConvert.SerializeObject(ViewModel.Juego));
        }

        private void Escuchar()
        {
            Console.WriteLine("Escuchando");
            while (true)
            {
                ViewModel.Actualizar();
                ViewModel.ActualizarInfoJugador();
            }
        }

    }
}
