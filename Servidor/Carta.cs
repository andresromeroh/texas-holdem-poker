using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Carta
    {
        [JsonProperty]
        public string Leyenda { get; set; }

        [JsonProperty]
        public int TipoPalo { get; set; }

        public static int ESPADAS = 0;
        public static int CORAZONES = 1;
        public static int DIAMANTES = 2;
        public static int TREBOLES = 3;


        // Constructor que no toma argumentos:
        public Carta()
        {
        }

        public int getValor()
        {
            int result = 0;
            try
            {
                result = Int32.Parse(this.Leyenda);
            }
            catch (FormatException)
            {
                switch (this.Leyenda)
                {
                    case "J":
                        result = 11;
                        break;
                    case "Q":
                        result = 12;
                        break;
                    case "K":
                        result = 13;
                        break;
                    case "A":
                        result = 14;
                        break;
                }
            }
            return result;
        }

        // Constructor que toma argumentos:
        public Carta(string leyenda, int tipoPalo)
        {
            Leyenda = leyenda;
            TipoPalo = tipoPalo;
        }

        public override string ToString()
        {
            string str = "";

            switch (TipoPalo)
            {
                case 0:
                    str = String.Format("{0} de ESPADAS", Leyenda);
                    break;

                case 1:
                    str = String.Format("{0} de CORAZONES", Leyenda);
                    break;

                case 2:
                    str = String.Format("{0} de DIAMANTES", Leyenda);
                    break;

                case 3:
                    str = String.Format("{0} de TREBOLES", Leyenda);
                    break;

                default:
                    break;
            }

            return str;
        }

    }
}
