using System;
using System.Runtime.Serialization;

namespace Gerontocracy.Core.Exceptions.Account
{
    public class EmailNotConfirmedException : Exception
    {
        public EmailNotConfirmedException()
        {
        }

        public EmailNotConfirmedException(string message) : base(message)
        {
        }

        public EmailNotConfirmedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmailNotConfirmedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
