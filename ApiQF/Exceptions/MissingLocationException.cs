using System;

namespace ApiQF.Exceptions
{
    public class MissingLocationException : Exception
    {
        public MissingLocationException(string message) : base(message)
        {
        }
    }
}