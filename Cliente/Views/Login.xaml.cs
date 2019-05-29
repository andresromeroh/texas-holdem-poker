using System;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using Cliente.Services;
using Newtonsoft.Json;

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
            string username = this.textBox_username.Text;
            string password = this.textBox_password.Text;

            Jugador jugador = new Jugador(username, password, 10000, true);
            string json = JsonConvert.SerializeObject(jugador);

            ClienteTCP.Init(servidor, puerto);
            ClienteTCP.Write(json);

            Thread.Sleep(5000);

            string jsonResponse = ClienteTCP.Read();
            bool loginExitoso = JsonConvert.DeserializeObject<bool>(jsonResponse);
            
            if(loginExitoso)
            {
                Mesa mesa = new Mesa();
                //Abrir la pantalla de mesa
                mesa.Show();
                //Cerrar la pantalla de login
                this.Close();
            }
            else
            {
                //Mostrar advertencia de credenciales incorrectos
                string message = "Username or password is incorrect, please try again.";
                string caption = "Error Detected in Credentials";
                MessageBoxButtons buttons = MessageBoxButtons.OK;

                //Muestra el mensaje
                System.Windows.Forms.MessageBox.Show(message, caption, buttons);
            }
        }
    }
}
