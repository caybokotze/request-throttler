using System;

namespace RequestThrottler
{
    public class ServiceNotImplementedException : Exception
    {
        public ServiceNotImplementedException() : base("The service for the caller has not been implemented or registered.")
        {
            
        }
    }
}