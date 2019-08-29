using System;
using System.Runtime.Serialization;

namespace Gerontocracy.Core.Exceptions.News
{
    public class SourceAlreadyAddedException : Exception
    {
        public SourceAlreadyAddedException()
        {
        }

        public SourceAlreadyAddedException(string message) : base(message)
        {
        }

        public SourceAlreadyAddedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SourceAlreadyAddedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
