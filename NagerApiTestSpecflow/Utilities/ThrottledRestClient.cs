using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using RestSharp;

namespace NagerResponse.Utilities
{
    public class ThrottledRestClient : RestClient
    {
        private readonly int _requestsPerMinute;
        private int _lastRequestTime;

        public ThrottledRestClient(int requestsPerMinute)
        {
            _requestsPerMinute = requestsPerMinute;
        }
        public override IRestResponse Execute(IRestRequest request)
        {
            int elapsedTime = Environment.TickCount - _lastRequestTime;
            int pause = (60 / _requestsPerMinute) * 1000;
            int wait = pause - elapsedTime;
            if (wait > 0)
            {
                Thread.Sleep(wait);
            }
            var response = base.Execute(request);
            _lastRequestTime = Environment.TickCount;
            return response;
        }
    }
}
