using Cliente.Services;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Windows;
using Microsoft.VisualBasic;

namespace Cliente
{
    public partial class Mesa : Window
    {
        private readonly ViewModel ViewModel;

        public Mesa()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            ViewModel = new ViewModel();
            DataContext = ViewModel;
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

        private void Raise(object sender, RoutedEventArgs e)
        {
            string fichasStr = Interaction.InputBox("Indique la cantidad de fichas a apostar:", "Subir Apuesta", "100");
            int fichas;

            if (fichasStr != "" && int.TryParse(fichasStr,out fichas))
            {
                ViewModel.Raise(fichas);
                ClienteTCP.Write(JsonConvert.SerializeObject(ViewModel.Juego));
            }
        }

        public void IniciarHilo()
        {
            Thread update = new Thread(Escuchar);
            update.Start();
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
