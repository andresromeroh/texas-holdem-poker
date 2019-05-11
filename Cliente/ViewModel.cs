using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Casa Casa;
        public ArrayList Jugadores;

        public ViewModel(Casa casa, ArrayList jugadores)
        {
            Casa = casa;
            Jugadores = jugadores;
        }

        public ViewModel()
        {
        }

    }
}
