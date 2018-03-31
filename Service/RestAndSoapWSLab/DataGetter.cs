using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestAndSoapWSLab
{
    class DataGetter
    {
        private static string URL_BASE = "https://api.jcdecaux.com/vls/v1/";
        private static string API_KEY = "7f86f599f65ccc092ee6446017eafb1307d1d94d";
        private static DataGetter instance = null;
        private DataTable cache = null;
        private WebRequest request = null;

       

        private int cacheTimeOut = 5;

        public DataGetter()
        {
            this.cache = new DataTable();
            cache.Columns.Add("ContractName", typeof(string));
            cache.Columns.Add("StationName", typeof(string));
            cache.Columns.Add("CompositeStation", typeof(CompositeStation));
            cache.Columns.Add("DateAdded", typeof(DateTime));
            MetricsGetter.GetInstance().OnNewCacheTimeout();
        }

        public static DataGetter GetInstance()
        {
            if (instance == null)
            {
                instance = new DataGetter();
            }
            return instance;
        }

        private string connectToServer(string url)
        {
            MetricsGetter.GetInstance().OnNewJCDecauxRequestMade();
           
            // Create a request for the URL.
            request = WebRequest.Create(url);
            
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = null;
            try
            {
                response = request.GetResponse();
                // Get the stream containing content returned by the server.
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string strResponse = reader.ReadToEnd();
                // Clean up the streams and the response.
                reader.Close();
                response.Close();
                return strResponse;
            }
            catch (System.Net.WebException)
            {
                return "";
            }
        }

        internal void setCacheTimeOutMinutes(int newValueMinutes)
        {
            cacheTimeOut = newValueMinutes;
        }

        internal int getCacheTimeOutMinutes()
        {
            return cacheTimeOut;
        }

        private List<CompositeContract> getContracts()
        {
            string url = URL_BASE + "contracts?apiKey="+API_KEY;
            string strResponse = connectToServer(url);
            JArray res = new JArray();
            try
            {
                res = JArray.Parse(strResponse);
            }
            catch (Newtonsoft.Json.JsonReaderException)
            {
                //let res empty because error
            }

            return res.ToObject<List<CompositeContract>>();
        }

        
        private JObject getStationInfos(int stationNumber, string contractName)
        {
            string url = URL_BASE + "stations/"+stationNumber+"?contract="+contractName+"&apiKey=" + API_KEY;
            string strResponse = connectToServer(url);
            JObject res = new JObject();
            try
            {
                res = JObject.Parse(strResponse);
            }
            catch (Newtonsoft.Json.JsonReaderException)
            {
                //let res empty because error
            }
            return res;
        }

        private JArray getStations()
        {
            string url = URL_BASE + "stations?apiKey=" + API_KEY;
            string strResponse = connectToServer(url);
            JArray res = new JArray();
            try
            {
                res = JArray.Parse(strResponse);
            }
            catch (Newtonsoft.Json.JsonReaderException)
            {
                //let res empty because error
            }
            return res;
        }

        private List<CompositeStation> getStationsOfContract(string contractName)
        {
            string url = URL_BASE + "stations?contract="+contractName+"&apiKey=" + API_KEY;
            string strResponse = connectToServer(url);
            JArray res = new JArray();
            List<CompositeStation> stations = new List<CompositeStation>();
            try
            {
                res = JArray.Parse(strResponse);
                foreach (JObject JStation in res) {
                    CompositeStation station = JsonConvert.DeserializeObject<CompositeStation>(JStation.ToString());
                    stations.Add(station);
                    cache.Rows.Add(contractName, station.Name,station, DateTime.Now);
                }
            }
            catch (Newtonsoft.Json.JsonReaderException)
            {
                //let res empty because error
            }
            return stations;
        }

        public CompositeStation getStationInfos(string contractName, string stationName)
        {
            foreach (CompositeStation station in getStations(contractName,stationName))
            {
                if (station.Name.ToLower().Contains(stationName.ToLower()))
                {
                    return station;
                }
            }
            return null;
        }

        private List<CompositeStation> getStations(string contractName, string stationName)
        {
            //if datatables with contractName and contains stationName take it in datatables
            List<CompositeStation> stationsForThisContract = new List<CompositeStation>();
            List<DataRow> rowsToDelete = new List<DataRow>();
            foreach (DataRow row in cache.Rows)
            {
                if(((DateTime) row["DateAdded"]).AddMinutes(cacheTimeOut).CompareTo(DateTime.Now)>=0){ // check if informations are below the expiration date
                    if (row["ContractName"].ToString().Equals(contractName) && (stationName=="" || (stationName != "" && row["StationName"].ToString().Contains(stationName))))
                    {
                        stationsForThisContract.Add(((CompositeStation)row["CompositeStation"]));
                        MetricsGetter.GetInstance().OnNewCacheTimeout();
                    }
                }else{ //otherwise erase it
                    rowsToDelete.Add(row);
                }
            }
            if(rowsToDelete.Count() > 0)
            {
                foreach(DataRow rowToDelete in rowsToDelete)
                {
                    rowToDelete.Delete();
                }
            }

            if(stationsForThisContract.Count() <= 0)
            {
                //else request api
                stationsForThisContract = getStationsOfContract(contractName);
            }
            return stationsForThisContract;
        }

        public List<string> getStationsNames(string contractName, string stationName)
        {
            List<string> names = new List<string>();
            if (stationName.Trim().Equals(""))
            {
                foreach (CompositeStation station in getStations(contractName,stationName))
                {
                    names.Add(station.Name);
                }
            }
            else
            {
                foreach (CompositeStation station in getStations(contractName, stationName))
                {
                    if (station.Name.ToLower().Contains(stationName.ToLower()))
                    {
                        names.Add(station.Name);
                    }
                }
            }
            return names;
        }

        public List<string> getContractsNames()
        {
            List<string> names = new List<string>();
            foreach (CompositeContract contract in getContracts())
            {
                names.Add(contract.Name);
            }
            return names;
        }

        public List<CompositeContract> getAllContracts()
        {
            List<CompositeContract> contracts = new List<CompositeContract>();
            foreach (CompositeContract contract in getContracts())
            {
                contracts.Add(contract);
            }
            return contracts;
        }

        public DataTable GetCache()
        {
            return this.cache;
        }
    }
}
