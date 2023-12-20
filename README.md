## Texas Hold'em Poker
Juego de Texas Hold'em en su versión sin limites para multiples jugadores de manera online.

Entre las tecnologías utilizadas para su desarrollo se encuentra C# .NET 4.7, Windows Presentation Foundation (WPF), JSON .NET y LDAP para autenticación con un controlador de dominios (Windows Server).

## Instalación
Para poder correr el proyecto se debe tener las siguientes herramientas:

 - VS Community 2019
 - .NET Framework 4.7
 - Newtonsoft JSON . NET [https://www.newtonsoft.com/json]
 - 2 VMs con Windows Server 2012 - 2016 se puede utilizar el "Free Tier" de Amazon Web Services [https://aws.amazon.com/es/free/]
 - Active Directory Domain Services
 
 Una vez con todos los requerimientos cada proyecto debe compilarse y ejecutarse dentro de VS Community en su respectivo entorno:
 - Servidor: Debe ejecutarse en unos de los VMs corriendo Windows Server.
 - Autenticacion: Este debe correrse en la misma VM que el servidor pero debera apuntar a la IP de la VM que esta corriendo los Domain Services mediante la función `Init()` de la siguiente manera.
 
 `Init() {ruta = "LDAP://IPADDRESS/CN=Users,DC=DOMAIN,DC=com";}`
 
 - Cliente: Debe ejecutarse en cualquier PC que vaya a participar del juego.
 
 ## Reglas del Juego
 El juego implementa las reglas regulares del juego Poker sin límites conocido mundialmente: https://www.pokerstars.com/espanol/poker/games/texas-holdem/?no_redirect=1#/sin%20l%C3%ADmite
