using System;
using System.Runtime.Serialization;

namespace Gerontocracy.Core.Exceptions.Affair
{
    public class AffairNotFoundException : Exception
    {
        public AffairNotFoundException()
        {
        }

        public AffairNotFoundException(string message) : base(message)
        {
        }

        public AffairNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AffairNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
