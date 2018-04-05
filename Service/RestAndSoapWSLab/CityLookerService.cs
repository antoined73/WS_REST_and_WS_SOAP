using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using RestAndSoapWSLab;

namespace EventsLib
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class CityLookerService : ICityLookerService
    {
        static Action<string, CompositeStation> m_Event1 = delegate { };
        static Action m_Event2 = delegate { };

        public void SubscribeNewNumberOfBikesEvent()
        {
            ICityLookerServiceEvents subscriber =
            OperationContext.Current.GetCallbackChannel<ICityLookerServiceEvents>();
            m_Event1 += subscriber.NewNumberOfBikes;
        }

        public void SubscribeNewNumberOfBikesFinishedEvent()
        {
            ICityLookerServiceEvents subscriber =
            OperationContext.Current.GetCallbackChannel<ICityLookerServiceEvents>();
            m_Event2 += subscriber.NewBikesFounded;
        }

        public void GetNumberOfBikes(string city, string stationName)
        {
            DataGetter dataGetter = DataGetter.GetInstance();
            CompositeStation station = dataGetter.getStationInfos(city, stationName);
            m_Event1(city, station);
            m_Event2();
        }
    }
}
