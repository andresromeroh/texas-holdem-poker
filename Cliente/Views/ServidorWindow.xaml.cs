using Cliente.Models;
using Cliente.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cliente
{
    /// <summary>
    /// Interaction logic for ServidorWindow.xaml
    /// </summary>
    public partial class ServidorWindow : Window
    {

        public ServidorWindow()
        {
            InitializeComponent();

        }

        private void conectarServidor(object sender, RoutedEventArgs e)
        {
            string servidor = this.textBox_servidor.Text;
            Int32 puerto = Int32.Parse(this.textBox_puerto.Text);

            TCPClientService clientService = new TCPClientService();
            clientService.startService(servidor, puerto);

            Juego juego = clientService.gameResponse();

            LoginWindow Login = new LoginWindow();
            Login.Init(clientService);

            //Abrir la pantalla de login
            Login.Show();

            //Cerrar la pantalla de conexion al servidor
            this.Close();
        }

    }
}
