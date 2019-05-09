using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Cliente
{
    class Casa
    {
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

        int JugadorDealer
        {
            get { return JugadorDealer; }
            set { JugadorDealer = value; }
        }

        int JugadorAlta
        {
            get { return JugadorAlta; }
            set { JugadorAlta = value; }
        }

        int JugadorMinima
        {
            get { return JugadorMinima; }
            set { JugadorMinima = value; }
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

        public Casa()
        {
        }

        public Casa(Stack mazo, ArrayList mesa, int jugadorDealer, int jugadorAlta, int jugadorMinima, int apuestaAlta, int apuestaMinima, int apuestaTotal)
        {
            Mazo = mazo;
            Mesa = mesa;
            JugadorDealer = jugadorDealer;
            JugadorAlta = jugadorAlta;
            JugadorMinima = jugadorMinima;
            ApuestaAlta = apuestaAlta;
            ApuestaMinima = apuestaMinima;
            ApuestaTotal = apuestaTotal;
        }

        

    }
}
