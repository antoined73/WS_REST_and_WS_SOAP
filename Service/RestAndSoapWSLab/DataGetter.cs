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


        public DataGetter()
        {
            this.cache = new DataTable();
            cache.Columns.Add("ContractName", typeof(string));
            cache.Columns.Add("StationName", typeof(string));
            cache.Columns.Add("StationJOBject", typeof(string));
            cache.Columns.Add("DateAdded", typeof(DateTime));
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
            // Create a request for the URL.
            request = WebRequest.Create(url);
            
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = null;
            try
            {
                response = request.GetResponse();
                ///////////////////request.BeginGetResponse(new AsyncCallback(FinishWebRequest), null);
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

        /**
        private void FinishWebRequest(IAsyncResult result)
        {
            WebResponse response = null;
            try
            {
                request.EndGetResponse(result);
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
        }**/

        private JArray getContracts()
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
            return res;
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

        private JArray getStationsOfContract(string contractName)
        {
            string url = URL_BASE + "stations?contract="+contractName+"&apiKey=" + API_KEY;
            string strResponse = connectToServer(url);
            JArray res = new JArray();
            try
            {
                res = JArray.Parse(strResponse);
                foreach (JObject station in res) {
                    cache.Rows.Add(contractName, station.GetValue("name"),res.ToString(), DateTime.Now);
                }
            }
            catch (Newtonsoft.Json.JsonReaderException)
            {
                //let res empty because error
            }
            return res;
        }

        public string getStationInfos(string contractName, string stationName)
        {
            foreach (JObject station in getStations(contractName,stationName))
            {
                if (station.GetValue("name").ToString().ToLower().Contains(stationName.ToLower()))
                {
                    return station.ToString();
                }
            }
            return "Aucune station portant ce nom a été trouvée";
        }

        private JArray getStations(string contractName, string stationName)
        {
            //if datatables with contractName and contains stationName take it in datatables
            JArray stationsForThisContract = new JArray();
            List<DataRow> rowsToDelete = new List<DataRow>();
            foreach (DataRow row in cache.Rows)
            {
                if(((DateTime) row["DateAdded"]).AddMinutes(5).CompareTo(DateTime.Now)<0){ // check if informations are below the expiration date
                    if (row["ContractName"].ToString().Equals(contractName) && (stationName != "" && row["StationName"].ToString().Contains(stationName)))
                    {
                        stationsForThisContract.Add(JObject.Parse(row["StationJObject"].ToString()));
                    }
                }else{ //otherwise erase it
                    rowsToDelete.Add(row);
                }
            }
            if(rowsToDelete.Count > 0)
            {
                foreach(DataRow rowToDelete in rowsToDelete)
                {
                    rowToDelete.Delete();
                }
            }

            if(stationsForThisContract.Count <= 0)
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
                foreach (JObject station in getStations(contractName,stationName))
                {
                    names.Add(station.GetValue("name").ToString());
                }
            }
            else
            {
                foreach (JObject station in getStations(contractName, stationName))
                {
                    if (station.GetValue("name").ToString().ToLower().Contains(stationName.ToLower()))
                    {
                        names.Add(station.GetValue("name").ToString());
                    }
                }
            }
            return names;
        }

        public List<string> GetContractsNames()
        {
            List<string> names = new List<string>();
            foreach (JObject contract in getContracts())
            {
                names.Add(contract.GetValue("name").ToString());
            }
            return names;
        }
    }
}
