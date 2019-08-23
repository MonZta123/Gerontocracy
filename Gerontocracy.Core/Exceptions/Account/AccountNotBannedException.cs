using System;
using System.Runtime.Serialization;

namespace Gerontocracy.Core.Exceptions.Account
{
    public class AccountNotBannedException : Exception
    {
        public AccountNotBannedException()
        {
        }

        public AccountNotBannedException(string message) : base(message)
        {
        }

        public AccountNotBannedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountNotBannedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
