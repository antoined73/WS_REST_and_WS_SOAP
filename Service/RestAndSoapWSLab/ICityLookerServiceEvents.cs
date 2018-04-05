using RestAndSoapWSLab;
using System;
using System.ServiceModel;

namespace EventsLib
{
    public interface ICityLookerServiceEvents
    {
        [OperationContract(IsOneWay = true)]
        void NewNumberOfBikes(string city, CompositeStation station);

        [OperationContract(IsOneWay = true)]
        void NewBikesFounded();
    }
}
