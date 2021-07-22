using System;

namespace ApiQF.Exceptions
{
    public class InvalidDistancesException : Exception
    {
        public InvalidDistancesException(string message) : base(message)
        {
        }
    }
}