using System;
using System.Runtime.Serialization;

namespace Gerontocracy.Core.Exceptions.Account
{
    public class CredentialException : Exception
    {
        public CredentialException()
        {
        }

        public CredentialException(string message) : base(message)
        {
        }

        public CredentialException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CredentialException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
