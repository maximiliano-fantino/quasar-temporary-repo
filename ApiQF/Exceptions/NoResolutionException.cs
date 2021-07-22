using System;

namespace ApiQF.Exceptions
{
    public class NoResolutionException : Exception
    {
        public NoResolutionException(string message) : base(message)
        {
        }
    }
}