using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Servidor
{
    public class Cliente
    {
        public TcpClient Socket;
        public Thread Thread;
        public StreamReader Reader;
        public StreamWriter Writer;
        public Jugador Jugador = null;

        //string Name;
        //int money;
        //int inroundmoney = 0;
        //int position = 50;
        //int hefresh = 0;
        //Carta[] mano = new Carta[2];

        public Cliente(TcpClient Socket)
        {
            this.Socket = Socket;
            Reader = new StreamReader(this.Socket.GetStream());
            Writer = new StreamWriter(this.Socket.GetStream());
            Writer.AutoFlush = true;
            Thread = new Thread(Login);
            Thread.Start();
        }

        private void Login() //Aqui se implementa la autenticacion Active Directory
        {
            while (Jugador == null)
            {
                try
                {
                    string json = Reader.ReadLine();
                    Jugador deserializedJugador = JsonConvert.DeserializeObject<Jugador>(json);

                    if (deserializedJugador != null)
                    {
                        Writer.WriteLine("1");
                        this.Jugador = deserializedJugador;
                        Lobby();
                    }
                    else
                    {
                        Writer.WriteLine("0");
                        Thread.Sleep(5000);
                    }
                }
                catch
                {
                    Disconnect();
                }
            }
        }

        public void Disconnect()
        {
            Reader.Close();
            Writer.Close();
            Socket.Close();
        }

        private void Lobby()
        {
            string rec;
            while ((rec = Reader.ReadLine()) != "Exit$")
            {
                try
                {
                    LobbyRequest(rec);
                }
                catch
                {
                    Disconnect();
                }
            }
            Disconnect();
        }

        private void LobbyRequest(string a)
        {
            string[] command = new string[3];
            for (int i = 0; a.IndexOf('$') != -1; i++)
            {
                command[i] = a.Substring(0, a.IndexOf('$'));
                a = a.Remove(0, a.IndexOf('$') + 1);
            }
            if (command[0] == "List")
            {
                string tmp = null;
                int i = 0;
                foreach (Table n in ServerLobby.Tables)
                {
                    tmp += i + "$" + n.ToString() + "@";
                    i++;
                }
                Writer.WriteLine(tmp);
            }
            else if (command[0] == "Spectate")
                ServerLobby.Tables[int.Parse(command[1])].Spectate(this);
            else if (command[0] == "Money")
                Writer.WriteLine(money);
        }
    }
}
