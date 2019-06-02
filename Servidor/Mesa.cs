using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Servidor
{
    class Mesa
    {
        public int Size; // Tamanno de la mesa
        public Juego Juego = null; // Objeto principal a serializar como respuesta continua
        public Thread ThreadJuego; // Thread de inicio de la partida
        public List<Cliente> ClientesJugador; // Lista de clientes actualmente conectados
        public int turno; // Num de turno

        public Mesa(int size, int apuestaMinima, int apuestaAlta)
        {
            Size = size;
            ClientesJugador = new List<Cliente>();

            Juego = new Juego
            {
                ApuestaMinima = apuestaMinima, // Definir la apuesta minima
                ApuestaAlta = apuestaAlta, // Definir la apuesta alta
                Bote = apuestaAlta + apuestaMinima // Definir el bote inicial
            };

            turno = 1;
        }

        public int AsignarAsiento() // Funcion para asignar asiento a un jugador
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
            Juego.Jugadores.Add(cliente.Jugador);

            cliente.Jugador.NumJugador = AsignarAsiento();
            cliente.Jugador.Role = Jugador.REGULAR;
            
            if (ClientesJugador.Count >= 1) // Necesarios 2 jugadores para comenzar
            {
                ThreadJuego = new Thread(IniciarJuego);
                ThreadJuego.Start();
            }
            else
            {
                Console.WriteLine("Esperando otro jugador, necesarios: 2\n");
                Thread.Sleep(60000); // Esperar 1 minutos a que se una otro jugador
                if (ClientesJugador.Count >= 1) // Necesarios 2 jugadores para comenzar
                {
                    if (Juego == null)
                    {
                        ThreadJuego = new Thread(IniciarJuego);
                        ThreadJuego.Start();
                    }
                    else if (!ThreadJuego.IsAlive)
                    {
                        ThreadJuego = new Thread(IniciarJuego);
                        ThreadJuego.Start();
                    }
                }
                else
                {
                    Console.WriteLine("Desconectando...\n");
                    cliente.Disconnect();
                    Remove(cliente);
                }
            }
        }

        public void Remove(Cliente cliente)
        {
            ClientesJugador.Remove(cliente);
            Juego.Jugadores.Remove(cliente.Jugador);

            Inform(JsonConvert.SerializeObject(Juego)); // Informar que un jugador ha sido removido

            if (Juego != null)
            {
                if (ThreadJuego.IsAlive && ClientesJugador.Count < 2)
                {
                    ThreadJuego.Abort();
                }
            }
        }

        public void IniciarJuego()
        {
            Console.WriteLine("JUEGO INICIADO!\n");

            while (true)
            {
                Juego.Repartir();
                Inform(JsonConvert.SerializeObject(Juego));
                RondaFlow();
                RondaTurn();
                RondaRiver();
            }
        }

        public void RondaFlow()
        {
            foreach (Cliente cliente in ClientesJugador)
            {
                if (cliente.Jugador.NumJugador == turno)
                {
                    Console.WriteLine("Waiting for data from player " + cliente.Jugador.NombreUsuario);
                    Juego = JsonConvert.DeserializeObject<Juego>(cliente.Reader.ReadLine());
                    Juego.Flop();
                    Inform(JsonConvert.SerializeObject(Juego));
                }
            }
        }

        public void RondaTurn()
        {
            foreach (Cliente cliente in ClientesJugador)
            {
                if (cliente.Jugador.NumJugador == turno)
                {
                    Console.WriteLine("Waiting for data from player " + cliente.Jugador.NombreUsuario);
                    Juego = JsonConvert.DeserializeObject<Juego>(cliente.Reader.ReadLine());
                    Juego.Turn();
                    Inform(JsonConvert.SerializeObject(Juego));
                }
            }
        }

        public void RondaRiver()
        {
            foreach (Cliente cliente in ClientesJugador)
            {
                if (cliente.Jugador.NumJugador == turno)
                {
                    Console.WriteLine("Waiting for data from player " + cliente.Jugador.NombreUsuario);
                    Juego = JsonConvert.DeserializeObject<Juego>(cliente.Reader.ReadLine());
                    Juego.River();
                    Inform(JsonConvert.SerializeObject(Juego));
                }
            }
        }

        public void Inform(string json)
        {
            foreach (Cliente cliente in ClientesJugador)
                cliente.Writer.WriteLine(json);
        } 

    }
}
