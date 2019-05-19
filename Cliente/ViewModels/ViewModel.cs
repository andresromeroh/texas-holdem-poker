using Cliente.Models;
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
        private Casa Casa;

        protected void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        Stack Mazo
        {
            get { return Mazo; }
            set { Mazo = value; }
        }

        ArrayList Mesa
        {
            get { return Mesa; }
            set { Mesa = value; }
        }

        ArrayList Participantes
        {
            get { return Participantes; }
            set { Participantes = value; }
        }

        int ApuestaAlta
        {
            get { return ApuestaAlta; }
            set { ApuestaAlta = value; }
        }

        int ApuestaMinima
        {
            get { return ApuestaMinima; }
            set { ApuestaMinima = value; }
        }

        int ApuestaTotal
        {
            get { return ApuestaTotal; }
            set { ApuestaTotal = value; }
        }
        

        public ViewModel()
        {
        }

        public ViewModel(Stack mazo, ArrayList mesa, ArrayList participantes, int apuestaAlta, int apuestaMinima, int apuestaTotal)
        {
            Mazo = mazo;
            Mesa = mesa;
            Participantes = participantes;
            ApuestaAlta = apuestaAlta;
            ApuestaMinima = apuestaMinima;
            ApuestaTotal = apuestaTotal;
        }


    }
}
