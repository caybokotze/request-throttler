using System;

namespace RequestThrottler
{
    public class RequestNotAllowedException : Exception
    {
        public RequestNotAllowedException(string message) : base(message)
        {
            
        }
    }
}