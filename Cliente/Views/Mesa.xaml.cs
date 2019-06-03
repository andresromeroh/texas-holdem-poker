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

            Thread update = new Thread(escuchar);
            update.Start();
        }

        private void call(object sender, RoutedEventArgs e)
        {
            ViewModel.Call();
            ClienteTCP.Write(JsonConvert.SerializeObject(ViewModel.Juego));
        }

        private void escuchar()
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
