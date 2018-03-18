using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RestAndSoapWSLab
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        CompositeStation getStationsInfos(string contractName, string stationName);

        [OperationContract]
        List<string> getStationsNames(string contractName, string stationName);

        [OperationContract]
        List<string> getContractsNames();

        [OperationContract]
        List<CompositeContract> getAllContracts();
    }


    [DataContract]
    public class CompositeStation
    {
        int number;
        string name;
        string address;
        Position position;
        bool banking;
        bool bonus;
        string status;
        string contract_name;
        int bike_stands;
        int available_bike_stands;
        int available_bikes;
        long last_update;

        [DataMember]
        public long Last_Update
        {
            get { return last_update; }
            set { last_update = value; }
        }

        [DataMember]
        public int Available_Bikes
        {
            get { return available_bikes; }
            set { available_bikes = value; }
        }

        [DataMember]
        public int Available_Bike_Stands
        {
            get { return available_bike_stands; }
            set { available_bike_stands = value; }
        }

        [DataMember]
        public int Bike_Stands
        {
            get { return bike_stands; }
            set { bike_stands = value; }
        }

        [DataMember]
        public string Contract_Name
        {
            get { return contract_name; }
            set { contract_name = value; }
        }

        [DataMember]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        [DataMember]
        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember]
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        [DataMember]
        public Position Position
        {
            get { return position; }
            set { position = value; }
        }

        [DataMember]
        public bool Banking
        {
            get { return banking; }
            set { banking = value; }
        }

        [DataMember]
        public bool Bonus
        {
            get { return bonus; }
            set { bonus = value; }
        }

    }

    [DataContract]
    public class Position
    {
        double lat;
        double lng;

        [DataMember]
        public double Lat
        {
            get { return lat; }
            set { lat = value; }
        }

        [DataMember]
        public double Lng
        {
            get { return lng; }
            set { lng = value; }
        }
    }

    [DataContract]
    public class CompositeContract
    {
        string name;
        List<string> cities;
        string commercial_name;
        string country_code;

        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember]
        public List<string> Cities
        {
            get { return cities; }
            set { cities = value; }
        }

        [DataMember]
        public string Commercial_Name
        {
            get { return commercial_name; }
            set { commercial_name = value; }
        }
        [DataMember]
        public string Country_Code
        {
            get { return country_code; }
            set { country_code = value; }
        }
    }
}
