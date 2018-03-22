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

        public MetricsData()
        {
            client = new ServiceReferenceMonitoring.ServiceMonitoringClient();
            
        }

        public void StartConstantRefreshing()
        {
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(1);

            var timer = new System.Threading.Timer((e) =>
            {
                RefreshData();
            }, null, startTimeSpan, periodTimeSpan);
        }

        public IDisposable Subscribe(IObserver<MetricsData> observer)
        {
            this.observer = observer;
            return null;
        }

        public void RefreshData()
        {
            numberOfRequest = client.getNumberOfRequests();
            if (observer != null)
            {
                observer.OnNext(this);
            }
        }

        public int GetNumberOfRequest()
        {
            return numberOfRequest;
        }
    }
}
