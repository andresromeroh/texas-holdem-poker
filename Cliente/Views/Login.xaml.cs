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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void conectarServidor(object sender, RoutedEventArgs e)
        {
            string servidor = this.textBox_servidor.Text;
            Int32 puerto = Int32.Parse(this.textBox_puerto.Text);
            /*
            TCPClientService clientService = new TCPClientService();
            clientService.startService(servidor, puerto);
            clientService.handleResponse();
            */
        }
    }
}
