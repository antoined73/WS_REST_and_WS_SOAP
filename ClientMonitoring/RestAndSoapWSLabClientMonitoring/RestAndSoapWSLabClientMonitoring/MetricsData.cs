using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAndSoapWSLabClientMonitoring
{
    public class MetricsData : IObservable<MetricsData>
    {
        IObserver<MetricsData> observer = null;
        ServiceReferenceMonitoring.ServiceMonitoringClient client = null;
        private int numberOfRequest = 0;
        private int numberOfJCDecauxRequest = 0;
        private int numberOfDataInCache;
        private float averageResponseTime;

        public MetricsData()
        {
            client = new ServiceReferenceMonitoring.ServiceMonitoringClient();
            
        }

        public IDisposable Subscribe(IObserver<MetricsData> observer)
        {
            this.observer = observer;
            return null;
        }

        public void RefreshData()
        {
            numberOfRequest = client.getNumberOfRequests();
            numberOfDataInCache = client.getNumberOfDataInCache();
            averageResponseTime = client.getAverageResponseTime();
            numberOfJCDecauxRequest = client.getNumberOfJCDecauxRequests();
            if (observer != null)
            {
                observer.OnNext(this);
            }
        }

        public int GetNumberOfRequest()
        {
            return numberOfRequest;
        }

        internal int GetNumberOfDataInCache()
        {
            return numberOfDataInCache;
        }

        internal float GetAverageResponseTime()
        {
            return averageResponseTime;
        }

        internal int GetNumberOfJCDecauxRequest()
        {
            return numberOfJCDecauxRequest;
        }
    }
}
