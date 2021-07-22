using System;

namespace ApiQF.Exceptions
{
    public class InvalidMessageException : Exception
    {
        public InvalidMessageException(string message) : base(message)
        {
        }
    }
}