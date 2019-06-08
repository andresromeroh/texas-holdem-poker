using System;
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
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void conectarServidor(object sender, RoutedEventArgs e)
        {
            string servidor = this.textBox_servidor.Text;
            Int32 puerto = Int32.Parse(this.textBox_puerto.Text);
            string username = this.textBox_username.Text;
            string password = this.textBox_password.Password;

            Jugador jugador = new Jugador(username, password, 10000, true);
            string json = JsonConvert.SerializeObject(jugador);

            ClienteTCP.Init(servidor, puerto, username);
            ClienteTCP.Write(json);

            string jsonResponse = ClienteTCP.Read();
            bool loginExitoso = JsonConvert.DeserializeObject<bool>(jsonResponse);
            
            if(loginExitoso)
            {
                //Mostrar mensaje de login exitoso
                string caption = "Inicio de Sesión Exitoso";
                string message = "Bienvenido a la mesa " + username + "!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;

                //Muestra el mensaje
                System.Windows.Forms.MessageBox.Show(message, caption, buttons);

                Mesa mesa = new Mesa();
                //Abrir la pantalla de mesa
                mesa.Show();
                //Cerrar la pantalla de login
                this.Close();
                mesa.IniciarHilo(); // Escuchar por cambios en el juego
            }
            else
            {
                //Mostrar advertencia de credenciales incorrectos
                string caption = "Error con los credenciales";
                string message = "El nombre de usuario o password es incorrecto!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;

                //Muestra el mensaje
                System.Windows.Forms.MessageBox.Show(message, caption, buttons);

                ClienteTCP.Disconnect();
            }
        }
    }
}
