using Cliente.Services;
using Newtonsoft.Json;
using System;
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
            ViewModel.ObtenerMano(ClienteTCP.Name());
        }

        private void call(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Esto es una prueba");
            ClienteTCP.Write(JsonConvert.SerializeObject(ViewModel.Juego));
            ViewModel.Actualizar();
        }

    }
}
