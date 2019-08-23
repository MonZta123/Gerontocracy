using System;
using System.Runtime.Serialization;

namespace Gerontocracy.Core.Exceptions.Account
{
    public class AccountAlreadyBannedException : Exception
    {
        public AccountAlreadyBannedException()
        {
        }

        public AccountAlreadyBannedException(string message) : base(message)
        {
        }

        public AccountAlreadyBannedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountAlreadyBannedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
