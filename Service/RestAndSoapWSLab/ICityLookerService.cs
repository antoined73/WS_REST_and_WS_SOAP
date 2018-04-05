using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace EventsLib
{
    [ServiceContract(CallbackContract = typeof(ICityLookerServiceEvents))]
    public interface ICityLookerService
    {
        [OperationContract]
        void GetNumberOfBikes(string city, string stationName);

        [OperationContract]
        void SubscribeNewNumberOfBikesEvent();

        [OperationContract]
        void SubscribeNewNumberOfBikesFinishedEvent();
    }

}
