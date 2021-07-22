using System;

namespace ApiQF.Exceptions
{
    public class GapOutOfRangeException : Exception
    {
        public GapOutOfRangeException(string message) : base(message)
        {
        }
    }
}