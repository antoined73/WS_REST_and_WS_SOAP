using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAndSoapWSLab
{
    public class ServiceMonitoring : IServiceMonitoring
    {
        public int getNumberOfRequests()
        {
            return MetricsGetter.GetInstance().getNumberOfRequests();
        }
    }
}
