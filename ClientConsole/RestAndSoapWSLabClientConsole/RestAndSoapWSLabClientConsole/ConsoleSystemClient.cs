using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAndSoapWSLabClientConsole
{
    class ConsoleSystemClient
    {
        private ServiceReference1.Service1Client client;

        public ConsoleSystemClient()
        {
            client = new ServiceReference1.Service1Client();
        }

        public void Launch()
        {
            WelcomeMessage();
            string command = "";
            do
            {
                Console.Write("#");
                command = Console.ReadLine();
                InterpretCommand(command);
            } while (!command.Equals("quit"));

        }

        private void InterpretCommand(string command)
        {
            string[] args = command.Split(' ');
            switch (args[0])
            {
                case "help":
                    DisplayHelp();
                    break;
                case "quit":
                    break;
                case "contracts":
                    DisplayContractsNames();
                    break;
                case "stations_names":
                    DisplayStationsNames(args);
                    break;
                case "stations_infos":
                    DisplayStationsInfos(args);
                    break;
                case "cache_timeout":
                    DisplayCacheTimeOut(args);
                    break;
                default:
                    Console.WriteLine("Commande non reconnue : " + command);
                    break;
            }
        }

        private void DisplayCacheTimeOut(string[] args)
        {
            if (args.Count() >= 2)
            {
                int newTimeOut = 5;
                int.TryParse(args[1], out newTimeOut);
                client.setCacheTimeOutMinutes(newTimeOut);
                Console.WriteLine("Timeout du cache modifié à "+newTimeOut+" minutes");
            }
            else
            {
                int timeOut = client.getCacheTimeOutMinutes();
                Console.WriteLine("Le timeout du cache vaut " + timeOut + " minutes");
            }
        }

        private void DisplayStationsInfos(string[] args)
        {
            if (args.Count() >= 2)
            {
                string contractName = args[1];
                string stationName = (args.Count() == 3 ? args[2] : "");
                ServiceReference1.CompositeStation stationInfo = client.getStationsInfos(contractName, stationName);
                Console.WriteLine(stationInfo.Name+"\nAdresse : "+stationInfo.Address+"\nNombre de vélos disponibles = "+stationInfo.Available_Bikes);
            }
            
        }

        private void DisplayStationsNames(string[] args)
        {
            if (args.Count() >= 2)
            {
                string contractName = args[1];
                string stationName = (args.Count() == 3 ? args[2] : "");

                string[] names = client.getStationsNames(contractName, stationName);
                foreach (string name in names)
                {
                    Console.WriteLine(name);
                }
                if (names.Count() <= 0)
                {
                    Console.WriteLine("Aucun nom de station trouvé.");
                }
            }
        }

        private void DisplayContractsNames()
        {
            foreach (string name in client.getContractsNames())
            {
                Console.WriteLine(name);
            }
        }

        private void WelcomeMessage()
        {
            Console.WriteLine("Bienvenue dans le client console pour le service SOAP IWS");
            DisplayHelp();
        }

        private void DisplayHelp()
        {
            Console.Write("Manuel d'utilisation :\n" +
                "help\n\tafficher cette aide.\n\n" +
                "contracts\n\tretourne la liste des contracts disponibles.\n\n" +
                "stations_names nom_de_contrat [nom_de_station]\n\tAffiche le nom de toutes les stations faisant partie du contract ayant pour nom \"nom_de_contrat\". Vous pouvez filtrez la recherche en trouvant seulement les stations contenant l'argument optionnel [nom_de_station].\n\n" +
                "stations_infos nom_de_contrat [nom_de_station]\n\tAffiche les informations de la première station trouvée correspondant aux critères donnés.\n\n" +
                "cache_timeout [valeur_entiere]\n\tSi l'argument optionnel \"valeur_entiere\" est utilise, le timeout du cache prends cette nouvelle valeur. Sinon affiche le timeout du cache en cours.\n\n" +
                "quit\n\tquitter le client console.\n\n");

        }
    }
}
