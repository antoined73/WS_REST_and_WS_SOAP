using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAndSoapWSLab
{
    public class ServiceMonitoring : IServiceMonitoring
    {
        public float getAverageResponseTime()
        {
            return MetricsGetter.GetInstance().getAverageResponseTime();
        }

        public int getNumberOfDataInCache()
        {
            return MetricsGetter.GetInstance().getNumberOfDataInCache();
        }

        public int getNumberOfJCDecauxRequests()
        {
            return MetricsGetter.GetInstance().getNumberOfJCDecauxRequests();
        }

        public int getNumberOfRequests()
        {
            return MetricsGetter.GetInstance().getNumberOfRequests();
        }
    }
}
