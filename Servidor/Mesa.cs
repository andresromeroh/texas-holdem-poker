using Newtonsoft.Json;
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
            ClientesJugador.Add(cliente);
            cliente.Jugador.NumJugador = FindNextSeat() - 1;
            Juego.Jugadores.Add(cliente.Jugador);

            //Informar a los demas jugadores luego de que un nuevo jugador se une
            //inform()
            
            if (ClientesJugador.Count >= 2) // Necesarios 2 jugadores para comenzar, pasar a 2 en produccion
            {
                ThreadJuego = new Thread(Game);
                ThreadJuego.Start();
            }
            else
            {
                Console.WriteLine("Esperando otro jugador, necesarios: 2\n");
                Thread.Sleep(120000); // Esperar 1 minutos a que se una otro jugador
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
                    Console.WriteLine("Desconectando...\n");
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
            Console.WriteLine("JUEGO INICIADO!!!\n");
            Juego.Repartir();
            Inform(JsonConvert.SerializeObject(Juego));
        }

        public void Inform(string json)
        {
            foreach (Cliente cliente in ClientesJugador)
                cliente.Writer.WriteLine(json);
        } 

    }
}
