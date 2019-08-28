using System;
using System.Runtime.Serialization;

namespace Gerontocracy.Core.Exceptions.Account
{
    public class AccountCannotBeBannedException : Exception
    {
        public AccountCannotBeBannedException()
        {
        }

        public AccountCannotBeBannedException(string message) : base(message)
        {
        }

        public AccountCannotBeBannedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountCannotBeBannedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
