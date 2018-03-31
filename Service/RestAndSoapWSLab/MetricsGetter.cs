using System;

namespace RestAndSoapWSLab
{
    public class MetricsGetter
    {
        private static MetricsGetter instance = null;
        private int numberOfJCDecauxRequests = 0;
        private int numberOfRequests = 0;
        private float totalTimePassed = 0;

        private DateTime startedTime;
        private DateTime stopedTime;

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

        internal float getAverageResponseTime()
        {
            return totalTimePassed / numberOfRequests;
        }

        internal int getNumberOfRequests()
        {
            return numberOfRequests;
        }

        public int getNumberOfDataInCache()
        {
            return DataGetter.GetInstance().GetCache().Rows.Count;
        }

        public int getNumberOfJCDecauxRequests()
        {
            return numberOfJCDecauxRequests;
        }

        public void OnNewJCDecauxRequestMade()
        {
            numberOfJCDecauxRequests++;
        }

        public void OnNewRequestStart()
        {
            numberOfRequests++;
            startedTime = DateTime.Now;
        }

        public void OnNewRequestEnd()
        {
            stopedTime = DateTime.Now;
            totalTimePassed += (float) (stopedTime.Subtract(startedTime).TotalMilliseconds);
        }

        internal void OnNewCacheTimeout()
        {
            //throw new NotImplementedException();
        }
    }
}