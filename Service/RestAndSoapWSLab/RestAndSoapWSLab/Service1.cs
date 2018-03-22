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
        public CompositeStation getStationsInfos(string contractName, string stationName)
        {
            DataGetter dataGetter = DataGetter.GetInstance();
            return dataGetter.getStationInfos(contractName,stationName);
        }

        public List<string> getStationsNames(string city, string stationName)
        {
            DataGetter dataGetter = DataGetter.GetInstance();
            return dataGetter.getStationsNames(city, stationName);
        }

        public List<string> getContractsNames()
        {
            DataGetter dataGetter = DataGetter.GetInstance();
            return dataGetter.getContractsNames();
        }

        public List<CompositeContract> getAllContracts()
        {
            DataGetter dataGetter = DataGetter.GetInstance();
            return dataGetter.getAllContracts();
        }

        public int getCacheTimeOutMinutes()
        {
            DataGetter dataGetter = DataGetter.GetInstance();
            return dataGetter.getCacheTimeOutMinutes();
        }

        public void setCacheTimeOutMinutes(int newValueMinutes)
        {
            DataGetter dataGetter = DataGetter.GetInstance();
            dataGetter.setCacheTimeOutMinutes(newValueMinutes);
        }
    }
}
