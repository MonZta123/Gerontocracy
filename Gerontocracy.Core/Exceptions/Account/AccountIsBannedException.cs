using System;
using System.Runtime.Serialization;

namespace Gerontocracy.Core.Exceptions.Account
{
    public class AccountIsBannedException : Exception
    {
        public AccountIsBannedException()
        {
        }

        public AccountIsBannedException(string message) : base(message)
        {
        }

        public AccountIsBannedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountIsBannedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
