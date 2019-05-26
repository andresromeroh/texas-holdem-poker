using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Servidor
{
    class Mesa
    {
        public int Size;
        public Juego Juego = null; // Objeto principal a serializar como respuesta continua
        public Thread ThreadJuego;
        public List<Cliente> ClientesJugador; // Lista de clientes actualmente conectados

        public Mesa(int size, int apuestaMinima)
        {
            this.Size = size; // Definir el tamanno de la mesa
            ClientesJugador = new List<Cliente>();
            Juego = new Juego();
            Juego.ApuestaMinima = apuestaMinima; // Definir la apuesta minima
        }

        public int FindNextSeat()
        {
            for (int i = 0; i < Size; i++)
            {
                bool check = true;
                foreach (Cliente cliente in ClientesJugador)
                {
                    if (cliente.Jugador.NumJugador == i)
                    {
                        check = false;
                    }
                }

                if (check)
                {
                    return i;
                }
            }

            return 0;
        }

        public void Add(Cliente cliente)
        {
            ClientesJugador.Append(cliente);
            cliente.Jugador.NumJugador = FindNextSeat();
            Juego.Jugadores.Append(cliente.Jugador);

            //Informar a los demas jugadores luego de que un nuevo jugador se une
            //inform()

            if (ClientesJugador.Count >= 2) // Necesarios 2 jugadores para comenzar
            {
                ThreadJuego = new Thread(Game);
                ThreadJuego.Start();
            }
            else
            {
                Thread.Sleep(120000); // Esperar 2 minutos a que se una otro jugador
                if (ClientesJugador.Count >= 2) // Necesarios 2 jugadores para comenzar
                {
                    if (Juego == null)
                    {
                        ThreadJuego = new Thread(Game);
                        ThreadJuego.Start();
                    }
                    else if (!ThreadJuego.IsAlive)
                    {
                        ThreadJuego = new Thread(Game);
                        ThreadJuego.Start();
                    }
                }
                else
                {
                    cliente.Disconnect();
                    /*
                    if (cliente.Pocket[0].Value != 0)
                    {
                        deck.Add(cliente.Pocket);
                    }
                    */
                    Remove(cliente);
                }
            }
        }

        public void Remove(Cliente cliente)
        {
            ClientesJugador.Remove(cliente);
            //Inform() informar que un jugador ha sido removido
            if (Juego != null)
                if (ThreadJuego.IsAlive && ClientesJugador.Count < 2)
                    ThreadJuego.Abort();
        }

        public void Game()
        {

        }

        public void Deal()
        {
            foreach (Jugador jugador in Juego.Jugadores)
            {
                jugador.Mano = repartirJugador();
                //jugador.Writer.WriteLine("Pocket$0$" + jugador.Pocket[0]); Enviar las cartas solo al jugador correspondiente
            }
        }

        public void Flop()
        {

        }

        public void Turn()
        {

        }

        public void River()
        {

        }

        public void Showdown()
        {

        }

        public void EarlyWin()
        {

        }

        public void PlayerMove(Cliente cliente)
        {

        }

        public void Collect()
        {

        }

        public Carta[] repartirJugador()
        {
            return null; //Aqui se requiere repartir 2 cartas del top del mazo
        }

        public void Inform(string json)
        {
            foreach (Cliente cliente in ClientesJugador)
                cliente.Writer.WriteLine(json);
        } 

    }
}
