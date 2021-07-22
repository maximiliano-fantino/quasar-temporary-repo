using System;

namespace ApiQF.Exceptions
{
    public class InvalidLocationException : Exception
    {
        public InvalidLocationException(string message) : base(message)
        {
        }
    }
}