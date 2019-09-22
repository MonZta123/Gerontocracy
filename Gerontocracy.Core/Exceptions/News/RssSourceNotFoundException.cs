using System;
using System.Runtime.Serialization;

namespace Gerontocracy.Core.Exceptions.News
{
    public class RssSourceNotFoundException : Exception
    {
        public RssSourceNotFoundException()
        {
        }

        public RssSourceNotFoundException(string message) : base(message)
        {
        }

        public RssSourceNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RssSourceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
