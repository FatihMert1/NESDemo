using System;

namespace NilveraDemo.Exceptions
{
    public class ResponseException: Exception
    {
        
        public ResponseException(string message) : base(message)
        {
        }
    }
}