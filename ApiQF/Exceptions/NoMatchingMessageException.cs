using System;

namespace ApiQF.Exceptions
{
    public class NoMatchingMessageException : Exception
    {
        public NoMatchingMessageException(string message) : base(message)
        {
        }
    }
}