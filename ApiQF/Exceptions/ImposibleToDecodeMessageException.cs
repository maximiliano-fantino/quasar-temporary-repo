using System;

namespace ApiQF.Exceptions
{
    public class ImposibleToDecodeMessageException : Exception
    {
        public ImposibleToDecodeMessageException(string message) : base(message)
        {
        }
    }
}