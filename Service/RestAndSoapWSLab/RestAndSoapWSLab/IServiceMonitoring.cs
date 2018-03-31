using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RestAndSoapWSLab
{
    [ServiceContract]
    public interface IServiceMonitoring
    {
        [OperationContract]
        int getNumberOfJCDecauxRequests();

        [OperationContract]
        int getNumberOfRequests();

        [OperationContract]
        int getNumberOfDataInCache();

        [OperationContract]
        float getAverageResponseTime();
    }

}
