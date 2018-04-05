using RestAndSoapWSLabClientConsole.CityLookerServiceReference;
using RestAndSoapWSLabClientConsole.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace EventsClient
{
    class CalcServiceCallbackSink: RestAndSoapWSLabClientConsole.CityLookerServiceReference.ICityLookerServiceCallback 
    {

        public void NewBikesFounded()
        {
            Console.WriteLine("New Bikes founded");
        }

        public void NewNumberOfBikes(string city, RestAndSoapWSLabClientConsole.CityLookerServiceReference.CompositeStation station)
        {
            Console.WriteLine(station.Name + "\nAdresse : " + station.Address + "\nNombre de vélos disponibles = " + station.Available_Bikes);
        }
    }
}

