using System;

namespace ApiQF.Exceptions
{
    public class InsuficcientSatellitesException : Exception
    {
        public InsuficcientSatellitesException(string message) : base(message)
        {
        }
    }
}