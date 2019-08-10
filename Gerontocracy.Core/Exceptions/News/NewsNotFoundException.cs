using System;
using System.Runtime.Serialization; 

namespace Gerontocracy.Core.Exceptions.News
{
    public class NewsNotFoundException : Exception
    {
        public NewsNotFoundException()
        {
        }

        public NewsNotFoundException(string message) : base(message)
        {
        }

        public NewsNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NewsNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
