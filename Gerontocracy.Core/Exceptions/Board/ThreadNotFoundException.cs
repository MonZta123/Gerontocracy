using System;
using System.Runtime.Serialization;

namespace Gerontocracy.Core.Exceptions.Board
{
    public class ThreadNotFoundException : Exception
    {
        public ThreadNotFoundException()
        {
        }

        public ThreadNotFoundException(string message) : base(message)
        {
        }

        public ThreadNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ThreadNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
