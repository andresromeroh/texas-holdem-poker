using Cliente.Services;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cliente
{
    /// <summary>
    /// Interaction logic for Mesa.xaml
    /// </summary>
    public partial class Mesa : Window
    {
        private readonly ViewModel ViewModel;

        public Mesa()
        {
            InitializeComponent();
            ViewModel = new ViewModel();
            // The DataContext serves as the starting point of Binding Paths
            DataContext = ViewModel;
            ViewModel.ObtenerMano(ClienteTCP.Name());
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
