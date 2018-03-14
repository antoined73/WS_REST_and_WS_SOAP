using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Newtonsoft.Json.Linq;

namespace RestAndSoapWSLab
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        public string getStationsInfos(string contractName, string stationName)
        {
            DataGetter dataGetter = DataGetter.GetInstance();
            return dataGetter.getStationInfos(contractName,stationName);
        }

        public List<string> getStationsNames(string city, string stationName)
        {
            DataGetter dataGetter = DataGetter.GetInstance();
            return dataGetter.getStationsNames(city, stationName);
        }

        public List<string> GetContractsNames()
        {
            DataGetter dataGetter = DataGetter.GetInstance();
            return dataGetter.GetContractsNames();
        }
    }
}
