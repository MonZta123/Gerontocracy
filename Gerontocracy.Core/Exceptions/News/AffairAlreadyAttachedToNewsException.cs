using System;
using System.Runtime.Serialization;

namespace Gerontocracy.Core.Exceptions.News
{
    public class AffairAlreadyAttachedToNewsException : Exception
    {
        public AffairAlreadyAttachedToNewsException()
        {
        }

        public AffairAlreadyAttachedToNewsException(string message) : base(message)
        {
        }

        public AffairAlreadyAttachedToNewsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AffairAlreadyAttachedToNewsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
