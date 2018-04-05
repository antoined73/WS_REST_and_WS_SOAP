using EventsClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RestAndSoapWSLabClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            CalcServiceCallbackSink objsink = new CalcServiceCallbackSink();
            InstanceContext iCntxt = new InstanceContext(objsink);

            CityLookerServiceReference.CityLookerServiceClient objClient = new CityLookerServiceReference.CityLookerServiceClient(iCntxt);
            objClient.SubscribeNewNumberOfBikesEvent();
            objClient.SubscribeNewNumberOfBikesFinishedEvent();

            ConsoleSystemClient console = new ConsoleSystemClient(objClient);
            console.Launch();

        }

    }
}
