using System;

namespace RestAndSoapWSLab
{
    public class MetricsGetter
    {
        private static MetricsGetter instance = null;
        private int numberOfRequests = 0;

        public MetricsGetter()
        {
        }

        public static MetricsGetter GetInstance()
        {
            if (instance == null)
            {
                instance = new MetricsGetter();
            }
            return instance;
        }

        public int getNumberOfRequests()
        {
            return numberOfRequests;
        }

        public void OnNewRequestMade()
        {
            numberOfRequests++;
        }

        internal void OnNewCacheTimeout()
        {
            //throw new NotImplementedException();
        }
    }
}