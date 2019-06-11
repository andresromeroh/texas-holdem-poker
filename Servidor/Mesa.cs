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

            switch (cliente.Jugador.NumJugador) // Definir las apuestas iniciales
            {
                case 1:
                    cliente.Jugador.Role = Jugador.APUESTA_BAJA;
                    cliente.Jugador.ApuestaActual = Juego.ApuestaMinima;
                    cliente.Jugador.CantFichas -= Juego.ApuestaMinima;
                    break;

                case 2:
                    cliente.Jugador.Role = Jugador.APUESTA_ALTA;
                    cliente.Jugador.ApuestaActual = Juego.ApuestaAlta;
                    cliente.Jugador.CantFichas -= Juego.ApuestaAlta;
                    break;

                default:
                    cliente.Jugador.Role = Jugador.REGULAR;
                    cliente.Jugador.ApuestaActual = 0;
                    break;
            }

            Juego.ActualizarInformacion(cliente.Jugador.NombreUsuario + " se ha unido a la mesa!\n");
            Informar();

            if (ClientesJugador.Count >= 2) // Necesarios 2 jugadores para comenzar
            {
                ThreadJuego = new Thread(IniciarJuego);
                ThreadJuego.Start();
            }
            else
            {
                Juego.ActualizarInformacion("Esperando por mas jugadores para iniciar...\n");
                Thread.Sleep(80000); // Esperar 1 minutos a que se una otro jugador
                if (ClientesJugador.Count >= 2) // Necesarios 2 jugadores para comenzar
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

            Informar(); // Informar que un jugador ha sido removido

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
            Juego.ActualizarInformacion("EL JUEGO HA INICIADO!\n");
            int ronda = 1;
            while (true)
            {
                Juego.DefinirApuestas();
                Juego.ActualizarInformacion("Ha Iniciado una nueva ronda!\n");
                Juego.Repartir();
                Juego.ActualizarInformacion("Se han repartido las cartas!\n");

                RondaPreFlop();
                RondaFlop();
                RondaTurn();
                RondaRiver();

                ObtenerGanadorRonda();
                Thread.Sleep(5000);

                Juego.ActualizarInformacion("Iniciando nueva ronda por favor espere...\n");
                RestablecerMesa();
                Informar();
                Thread.Sleep(10000);

                ronda++;
            }
        }

        public void RondaPreFlop()
        {
            foreach (Cliente cliente in ClientesJugador)
            {
                ActualizarEstadoJugador(cliente, Jugador.JUGANDO);
                Juego.ActualizarInformacion("Turno del jugador: " + cliente.Jugador.NombreUsuario + "\n");
                Informar();
                Juego = JsonConvert.DeserializeObject<Juego>(cliente.Reader.ReadLine());
                Informar();
                ActualizarEstadoJugador(cliente, Jugador.ESPERANDO);
                Juego.ActualizarInformacion("Ha finalizado el turno del jugador: " + cliente.Jugador.NombreUsuario + "\n");
            }

            Juego.SacarFlop(); // Una vez ya todos han jugado
            Juego.ActualizarInformacion("Mostrando el Flop... \n");
            Informar();
        }

        public void RondaFlop()
        {
            foreach (Cliente cliente in ClientesJugador)
            {
                ActualizarEstadoJugador(cliente, Jugador.JUGANDO);
                Juego.ActualizarInformacion("Turno del jugador: " + cliente.Jugador.NombreUsuario + "\n");
                Informar();
                Juego = JsonConvert.DeserializeObject<Juego>(cliente.Reader.ReadLine());
                Informar();
                ActualizarEstadoJugador(cliente, Jugador.ESPERANDO);
                Juego.ActualizarInformacion("Ha finalizado el turno del jugador: " + cliente.Jugador.NombreUsuario + "\n");
            }

            Juego.SacarTurn(); // Una vez ya todos han jugado
            Juego.ActualizarInformacion("Mostrando el Turn... \n");
            Informar();
        }

        public void RondaTurn()
        {
            foreach (Cliente cliente in ClientesJugador)
            {
                ActualizarEstadoJugador(cliente, Jugador.JUGANDO);
                Juego.ActualizarInformacion("Turno del jugador: " + cliente.Jugador.NombreUsuario + "\n");
                Informar();
                Juego = JsonConvert.DeserializeObject<Juego>(cliente.Reader.ReadLine());
                Informar();
                ActualizarEstadoJugador(cliente, Jugador.ESPERANDO);
                Juego.ActualizarInformacion("Ha finalizado el turno del jugador: " + cliente.Jugador.NombreUsuario + "\n");
            }

            Juego.SacarRiver(); // Una vez ya todos han jugado
            Juego.ActualizarInformacion("Mostrando el River... \n");
            Juego.ActualizarInformacion("Se procede a hacer las puestas finales... \n");
            Informar();
        }

        public void RondaRiver()
        {
            foreach (Cliente cliente in ClientesJugador)
            {
                ActualizarEstadoJugador(cliente, Jugador.JUGANDO);
                Juego.ActualizarInformacion("Turno del jugador: " + cliente.Jugador.NombreUsuario + "\n");
                Informar();
                Juego = JsonConvert.DeserializeObject<Juego>(cliente.Reader.ReadLine());
                Informar();
                ActualizarEstadoJugador(cliente, Jugador.ESPERANDO);
                Juego.ActualizarInformacion("Ha finalizado el turno del jugador: " + cliente.Jugador.NombreUsuario + "\n");
            }

            Informar();
        }

        public void ActualizarEstadoJugador(Cliente cliente, string estado)
        {
            foreach (Jugador jugador in Juego.Jugadores)
            {
                if (jugador.NombreUsuario.Equals(cliente.Jugador.NombreUsuario))
                {
                    if (estado.Equals(Jugador.JUGANDO))
                    {
                        jugador.Estado = estado;
                        Juego.Turno = jugador.NumJugador;
                    }
                    else
                    {
                        jugador.Estado = estado;
                    }
                }
            }
        }

        public void ObtenerGanadorRonda() // Se analizan los distintos juegos
        {
            Juego.ActualizarInformacion("Turno final, se procede a mostrar los juegos...\n");
            Juego.ActualizarInformacion("Cartas en la mesa: \n");

            foreach (Carta carta in Juego.CartasComunes)
            {
                Juego.ActualizarInformacion(carta.ToString() + "; ");
            }

            foreach (Jugador jugador in Juego.Jugadores)
            {
                Juego.ActualizarInformacion(jugador.NombreUsuario + " tiene el siguiente juego: " + jugador.Mano[0].ToString() + " y " + jugador.Mano[1].ToString() + "\n");
            }

            Jugador ganador = Juego.EncontrarGanador();
            ganador.sumarFichas(Juego.Bote);
            Juego.Bote = 0;
            Juego.Turno = 0;

            Juego.ActualizarInformacion("El ganador de la ronda es: " + ganador.NombreUsuario + "\n");
            JugadaGanadora(ganador.PuntajeMano);

            Informar();
        }

        public void RestablecerMesa()
        {
            Juego.CartasComunes.Clear();
            Juego.Mazo.Clear();
            Juego.LlenarMazo();
            Juego.Ronda = 0;

            foreach (Jugador jugador in Juego.Jugadores)
            {
                jugador.ApuestaActual = 0;
                jugador.Role = Jugador.REGULAR;
                jugador.Mano = new Carta[2];
            }
        }

        public void Informar()
        {
            foreach (Cliente cliente in ClientesJugador)
                cliente.Writer.WriteLine(JsonConvert.SerializeObject(Juego));
        } 

        public void JugadaGanadora(int puntajeMano)
        {
            switch (puntajeMano)
            {
                case 1:
                    Juego.ActualizarInformacion("Ha ganado con carta alta!\n");
                    break;

                case 2:
                    Juego.ActualizarInformacion("Ha ganado con un par!\n");
                    break;

                case 3:
                    Juego.ActualizarInformacion("Ha ganado con doble par!\n");
                    break;

                case 4:
                    Juego.ActualizarInformacion("Ha ganado con trio!\n");
                    break;

                case 5:
                    Juego.ActualizarInformacion("Ha ganado con escalera!\n");
                    break;

                case 6:
                    Juego.ActualizarInformacion("Ha ganado con color!\n");
                    break;

                case 7:
                    Juego.ActualizarInformacion("Ha ganado con Full House!\n");
                    break;

                case 8:
                    Juego.ActualizarInformacion("Ha ganado con Poker!\n");
                    break;

                case 9:
                    Juego.ActualizarInformacion("Ha ganado con escalera de colores!\n");
                    break;

                case 10:
                    Juego.ActualizarInformacion("Ha ganado con escalera real!\n");
                    break;

                default:
                    break;
            }
        }

    }
}
