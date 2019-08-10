using System;
using System.Runtime.Serialization;

namespace Gerontocracy.Core.Exceptions.Account
{
    public class EmailAlreadyConfirmedException : Exception
    {
        public EmailAlreadyConfirmedException()
        {
        }

        public EmailAlreadyConfirmedException(string message) : base(message)
        {
        }

        public EmailAlreadyConfirmedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmailAlreadyConfirmedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
