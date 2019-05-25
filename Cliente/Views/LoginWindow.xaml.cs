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

namespace Cliente.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        TCPClientService ClientService = null;
        public LoginWindow()
        {
            InitializeComponent();
        }

        public void Init(TCPClientService clientService)
        {
            this.ClientService = clientService;
        }

        private void login(object sender, RoutedEventArgs e)
        {
            //
        }

    }
}
