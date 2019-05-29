using System;
using System.DirectoryServices;

namespace ADAutenticacion
{
    public class Autenticacion
    {
        static string ruta;

        public static void Init()
        {
            ruta = "LDAP://3.18.195.223/CN=Users,DC=sistemasoperativos,DC=com";
        }

        public static bool AutenticarUsuario(string username, string password)
        {
            bool exitoso = false;

            try
            {
                DirectoryEntry entry = new DirectoryEntry(ruta, username, password);
                object nativeObject = entry.NativeObject;
                exitoso = true;
            }
            catch (DirectoryServicesCOMException d)
            {
                Console.WriteLine(d.ExtendedErrorMessage);
            }
            return exitoso;
        }

        public static bool CrearUsuario(String nombre, String apellido, String username, String password)
        {
            DirectoryEntry container = new DirectoryEntry(ruta, "Administrator@sistemasoperativos.com", "Sistemasoperativos01", AuthenticationTypes.Secure);
            DirectoryEntry newUser = container.Children.Add("CN=" + username, "User");

            newUser.Properties["userprincipalname"].Add(username + "@sistemasoperativos.com");
            newUser.Properties["displayname"].Add(nombre + " " + apellido);
            newUser.Properties["givenName"].Value = nombre;
            newUser.Properties["sn"].Value = apellido;

            newUser.CommitChanges();

            //newUser.Invoke("setpassword", password);
            //newUser.Properties["userAccountControl"].Value = 0x0200;
            //newUser.CommitChanges();

            Console.WriteLine("Se creo el usuario: " + username);

            return true;
        }
    }
}
